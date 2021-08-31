using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sist_Inspeccion_GyS.Model;
using Sist_Inspeccion_GyS.Reporte;
using CrystalDecisions.CrystalReports.Engine;

namespace Sist_Inspeccion_GyS.Model
{
    public class SqliteDataAccess
    {

        //Metodo para cargar los datos de las inspecciones
        public static List<DataGridValues> LoadInspecciones()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DataGridValues>("select folio, folio||  '-'  || foliosalida as foliosalida, folio || '-' || folioentrada as folioentrada from Inspeccion where foliosalida IS NOT NULL", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<DataGridValues> BuscarInspecciones(string cadena)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DataGridValues>("select folio, folio||  '-'  || foliosalida as foliosalida, folio || '-' || folioentrada as folioentrada from Inspeccion where folio||  '-'  || foliosalida LIKE '%" + cadena + "%' OR folio || '-' || folioentrada LIKE '%" + cadena + "%' AND foliosalida IS NOT NULL", new DynamicParameters());
                return output.ToList();
            }
        }
        public static int SaveInspeccionRelacion(InspeccionRelacion inspeccionRelacion)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("insert into Inspeccion (folioentrada, foliosalida) values (@folioentrada, @foliosalida)", inspeccionRelacion);
                transaction.Commit();
                return cnn.Changes;

            }
        }
        public static int DeleteInspeccionRelacion(int folio)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("delete from Inspeccion where folio = '" + folio + "'");
                transaction.Commit();
                return cnn.Changes;
            }
        }
        public static bool RevisarSiFolioRelacionVacio()
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from Inspeccion where foliosalida IS NULL order by folio DESC LIMIT 1";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //sirve para encontrar el path
        public static string EncontrarFechaInspeccion(string folio, string tipo)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                SQLiteCommand cmd = cnn.CreateCommand();
                SQLiteTransaction transaction = null;
                if (tipo == "salida")
                {
                    cmd.CommandText = "select fecha from InspeccionSalida where folio  = '" + folio+ "'";
                }
                else
                {
                    cmd.CommandText = "select fecha from InspeccionEntrada where folio  = '" + folio+ "'";
                }
                transaction = cnn.BeginTransaction();
                string fecha = cmd.ExecuteScalar().ToString();
                transaction.Commit();
                return fecha;

            }
        }
        public static void DeleteInspeccionSalidaRelacion(string foliosalida)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("delete from InspeccionSalida where folio = '" + foliosalida + "'");
                transaction.Commit();

                //return cnn.Changes;

            }
        }


        public static int Update_Inspeccion_Salida(int foliorelacion)
        {
            //string nulo = "";
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Inspeccion " +
                    "set foliosalida = NULL where folio = '" + foliorelacion + "'");
                transaction.Commit();

                return cnn.Changes;
            }

        }

        //Metodo para buscar el folio con no.de caja mas reciente.
        public static int Update_Inspeccion_Entrada(int foliorelacion)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Inspeccion " +
                    "set folioentrada = NULL where folio = '" + foliorelacion + "'");
                transaction.Commit();

                return cnn.Changes;
            }

        }


        public static void DeleteInspeccionEntrada(string folioentrada)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("delete from InspeccionEntrada where folio = '" + folioentrada + "'");
                transaction.Commit();

                //return cnn.Changes;

            }
        }


        public static string Salida_Pendiente(string foliosalida)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                SQLiteCommand cmd = cnn.CreateCommand();
                SQLiteTransaction transaction = null;
                cmd.CommandText = "select folioentrada from Inspeccion where foliosalida  = '" + foliosalida + "'";
                transaction = cnn.BeginTransaction();
                string result = cmd.ExecuteScalar().ToString();
                transaction.Commit();


                return result;

            }

        }





        //Metodo para buscar el folio con no.de caja mas reciente.
        public static string BusquedaSalidaCaja(int nocaja)
        {
            //Se valida que el número de caja dado si ha tenido salidas.
            if(TieneSalida(nocaja))
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {

                    cnn.Open();
                    SQLiteCommand cmd = cnn.CreateCommand();
                    SQLiteTransaction transaction = null;
                    cmd.CommandText = "select folio from InspeccionSalida where nocaja  = '" + nocaja + "' order by rfolios DESC LIMIT 1";
                    transaction = cnn.BeginTransaction();
                    string result = cmd.ExecuteScalar().ToString();
                    transaction.Commit();


                    return result;

                }
            }
            else
            {
                throw new Exception("La caja indicada no ha tenido salida");
            }
            

        }
        #region metodos para verificar si hay entrada en salida caja



        public static string BusquedaEntradaCaja(int nocaja)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                SQLiteCommand cmd = cnn.CreateCommand();
                SQLiteTransaction transaction = null;
                cmd.CommandText = "select folio from InspeccionEntrada where nocaja  = '" + nocaja + "' order by rfolioe DESC LIMIT 1";
                transaction = cnn.BeginTransaction();
                string result = cmd.ExecuteScalar().ToString();
                transaction.Commit();


                return result;

            }

        }

        public static int BusquedaFolioEntrada(string folioentrada)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select folio from Inspeccion where folioentrada = '" + folioentrada + "'";
                transaction = cnn.BeginTransaction();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
                return result;
            }

        }

        public static bool VerificarNoHayCajaEntrada(int nocaja)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from InspeccionEntrada where nocaja ='" + nocaja + "'";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TieneSalida(int nocaja)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from InspeccionSalida where nocaja ='" + nocaja + "'";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerificarQueNoHayEntrada(int foliorelacion)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from Inspeccion where folio ='" + foliorelacion + "'and folioentrada is null";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        #endregion


        public static bool VerificarQueHaySalida(int foliorelacion)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from Inspeccion where folio ='" + foliorelacion + "'and folioentrada is null";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    
        //metodo para relacionar inspeccion salida con inspeccion relacion.
        public static int Update_InspeccionR_Salida(int folioeditar, string rfolios)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update InspeccionSalida " +
                    "set rfolios = '" + folioeditar + "' where folio = '" + rfolios + "'");
                transaction.Commit();

                return cnn.Changes;
            }

        }

        public static int Update_InspeccionR_Entrada(int folioeditar, string rfolios)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update InspeccionEntrada " +
                    "set rfolioe = '" + folioeditar + "' where folio = '" + rfolios + "'");
                transaction.Commit();

                return cnn.Changes;
            }

        }


        //Metodo para buscar el folio(consecutivo) que se inserto en InspeccionRelacion con el folio de salida. (compuesto)

        public static int BusquedaFolioRelacion(string foliosalida)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select folio from Inspeccion where foliosalida = '" + foliosalida + "'";
                transaction = cnn.BeginTransaction();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
                return result;
            }

        }

        public static int InsercionRelacion(int foliorelacion, string folioentrada)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Inspeccion " +
                    "set folioentrada = '" + folioentrada + "' where folio = '" + foliorelacion + "'");
                transaction.Commit();

                return cnn.Changes;
            }

        }

        //metodo para buscar el consecutivo de Llantas.

        public static long BusquedaFolioConsecutivoLlantas(string foliosalida)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select folio from Llantas where foliosalida = '" + foliosalida + "'";
                transaction = cnn.BeginTransaction();
                int result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
                return result;
            }

        }



        //Metodo para agregr nuevas inspecciones
        public static int SaveInspeccionEntrada(Inspeccion inspeccionentrada)
        {
            if(!RevisarSiEntradaRepetida(inspeccionentrada.folio))
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    SQLiteTransaction transaction = null;
                    transaction = cnn.BeginTransaction();
                    //crear base de datos desde 0.
                    cnn.Execute("insert into InspeccionEntrada" +
                        "(folio, nocaja, fecha, hora, cliente,nombreoperador, inspector, unidadmedida, temperaturaprogramada, temperaturareal, idimagenes, idluces, idperifericos, idllantas, idcompartimientosocultos) values " +
                        "(@folio, @nocaja, @fecha, @hora, @cliente, @nombreoperador, @inspector, @unidadmedida, @temperaturaprogramada, @temperaturareal, @idimagenes, @idluces, @idperifericos, @idllantas, @idcompartimientosocultos)", inspeccionentrada);
                    transaction.Commit();
                    return cnn.Changes;
                }
            }
            else
            {
                throw new Exception("Inspección repetida");
            }
        }
        static bool RevisarSiEntradaRepetida(string folioentrada)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from InspeccionEntrada where folio ='" + folioentrada + "'";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Metodo para agregar  inspecciones de salida
        public static int SaveInspeccionSalida(Inspeccion inspeccionsalida)
        {
            if(!RevisarSiSalidaRepetida(inspeccionsalida.folio))
            {
                using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    SQLiteTransaction transaction = null;
                    transaction = cnn.BeginTransaction();
                    //crear base de datos desde 0.
                    cnn.Execute("insert into InspeccionSalida" +
                        "(folio, nocaja, fecha, hora, cliente,nombreoperador, inspector,unidadmedida,temperaturaprogramada,temperaturareal, idimagenes, idluces, idperifericos, idllantas, idcompartimientosocultos) values " +
                        "(@folio, @nocaja, @fecha, @hora, @cliente, @nombreoperador, @inspector, @unidadmedida,@temperaturaprogramada,@temperaturareal,@idimagenes, @idluces, @idperifericos, @idllantas, @idcompartimientosocultos)", inspeccionsalida);
                    transaction.Commit();
                    return cnn.Changes;
                }
            }
            else
            {
                throw new Exception("Inspección repetida");
            }
        }
        static bool RevisarSiSalidaRepetida(string foliosalida)
        {
            int result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select count(*) from InspeccionSalida where folio ='" + foliosalida + "'";
                transaction = cnn.BeginTransaction();
                result = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool RevisarSiSalidaTieneEntrada(string nocaja)
        {
            string result;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select folio from InspeccionEntrada where nocaja ='" + nocaja + "'";
                transaction = cnn.BeginTransaction();
                result = (cmd.ExecuteScalar().ToString());
                transaction.Commit();
            }
            if (result == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //metodo para buscar el folio con el consecutivo en Inspeccion.
        public static string Buscar_FolioSalida_Editar(int folioconsecutivo)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select foliosalida from Inspeccion where folio = '" + folioconsecutivo + "'";
                transaction = cnn.BeginTransaction();
                string result = cmd.ExecuteScalar().ToString();
                transaction.Commit();
                return result;
            }
        }
        public static string Buscar_FolioEntrada_Editar(int folioconsecutivo)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select folioentrada from Inspeccion where folio = '" + folioconsecutivo + "'";
                transaction = cnn.BeginTransaction();
                string result = cmd.ExecuteScalar().ToString();
                transaction.Commit();
                return result;
            }
        }

        public static void EditarSalida(int folioconsecutivo, string foliosalida)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Inspeccion " +
                    "set foliosalida = '" + foliosalida + "' where folio = '" + folioconsecutivo + "'");
                transaction.Commit();

            }

        }
        public static int Ultimo_FolioRelacion_Insertado()
        {
            int id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cmd.CommandText = "select folio from Inspeccion order by folio DESC LIMIT 1";
                id = int.Parse(cmd.ExecuteScalar().ToString());
                transaction.Commit();
                return id;
            }
        }
        public static string Ultimo_FolioEntrada()
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                SQLiteCommand cmd = cnn.CreateCommand();
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cmd.CommandText = "select folio from InspeccionEntrada order by rfolioe DESC LIMIT 1";
                string folio= cmd.ExecuteScalar().ToString();
                transaction.Commit();
                return folio;
            }
        }
        #region Metodos IMAGENES
        //Metodo para insertar Imagenes

        public static long SaveImagenes(Imagenes imagenes)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into Imagenes" +
                    "(ImgCajaFrontal, ImgCajaTrasera, ImgCajaDerecha, ImgCajaIzquierda, ImgDiesel, ImgTermometro, rfoliosalida, rfolioentrada) values " +
                    "(@ImgCajaFrontal, @ImgCajaTrasera, @ImgCajaDerecha, @ImgCajaIzquierda, @ImgDiesel, @ImgTermometro,@rfoliosalida, @rfolioentrada)", imagenes);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }



        //metodo para la relacion rfoliosalida = rfolioentrada.
        public static void Insercion_Relacion_Imagenes_Salida_Entrada(string rfoliosalida, string rfolioentrada)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Imagenes " +
                    "set rfolioentrada = '" + rfolioentrada + "' where rfoliosalida = '" + rfoliosalida + "'");
                transaction.Commit();

            }

        }


        #endregion

        #region Metodos LUCES
        //Metodo para insertar luces de caja
        public static long SaveLuces(Luces luces)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("insert into Luces" +
                    "(fderecha, cderecha, frderecha, dirderecha, plafon, fizquierda, cizquierda, frizquierda, dirizquierda, abs, observacionesluces, rfoliosalida, rfolioentrada) values " +
                    "(@fderecha, @cderecha, @frderecha, @dirderecha, @plafon, @fizquierda, @cizquierda, @frizquierda , @dirizquierda , @abs, @observacionesluces, @rfoliosalida, @rfolioentrada  )", luces);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }

        //update para insertar folioentrada
        public static void Insercion_Relacion_Luces_Salida_Entrada(string rfoliosalida, string rfolioentrada)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Luces " +
                    "set rfolioentrada = '" + rfolioentrada + "' where rfoliosalida = '" + rfoliosalida + "'");
                transaction.Commit();

            }

        }



        #endregion

        #region metodos PERIFERICOS
        //Metodo para insertar perifericos
        public static long SavePerifericos(Perifericos perifericos)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into Perifericos" +
                    "(Manivela, LoderaIzq, LoderaDerecha, ManitasAire, RinesRemolque, ManguerasAire, rfoliosalida, rfolioentrada) values " +
                    "(@Manivela, @LoderaIzq, @LoderaDerecha, @ManitasAire, @RinesRemolque, @ManguerasAire, @rfoliosalida, @rfolioentrada)", perifericos);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }


        public static void Insercion_Relacion_Perifericos_Salida_Entrada(string rfoliosalida, string rfolioentrada)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update Perifericos " +
                    "set rfolioentrada = '" + rfolioentrada + "' where rfoliosalida = '" + rfoliosalida + "'");
                transaction.Commit();

            }

        }




        #endregion region


        #region metodos COMPARTIMIENTOS
        //Metodo para insertar compartimientos ocultos
        public static long SaveCompartimientos(CompartimientosOcultos compartimientos)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into CompartimientosOcultos" +
                    "(Defensa, Motor, Llantas, PisoCabina, TanqueCombustible, Cabina, CilindrosAire, Cambios, QuintaRueda, ExteriorChasis, " +
                    "PisoRemolqueAdentro, PuertasAdentroAfuera, ParedesLados, TechoParedFrontal, ParedFrontalPiso, UnidadRefrigerada, Mofle, Fleje, rfoliosalida, rfolioentrada ) values " +
                    "(@Defensa, @Motor, @Llantas, @PisoCabina , @TanqueCombustible ,@Cabina,@CilindrosAire,@Cambios,@QuintaRueda," +
                    "@ExteriorChasis,@PisoRemolqueAdentro,@PuertasAdentroAfuera,@ParedesLados,@TechoParedFrontal,@ParedFrontalPiso,@UnidadRefrigerada,@Mofle,@Fleje," +
                    "@rfoliosalida, @rfolioentrada)", compartimientos);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }


        public static void Insercion_Relacion_Compartimiento_Salida_Entrada(string rfoliosalida, string rfolioentrada)
        {

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update CompartimientosOcultos " +
                    "set rfolioentrada = '" + rfolioentrada + "' where rfoliosalida = '" + rfoliosalida + "'");
                transaction.Commit();

            }

        }

        #endregion



        #region LLantas
        //Metodo para llantas de la caja

        //Llanta11
        public static long SaveLlanta11(LlantaIzqExt11 llanta11)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaIzqExt11" +
                    "(marcallanta11, condicionllanta11, rllanta) values " +
                    "(@marcallanta11, @condicionllanta11, @rllanta11)", llanta11);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta12

        public static long SaveLlanta12(LlantaIzqInt12 llanta12)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaIzqInt12" +
                    "(marcallanta12, condicionllanta12 , rllanta) values " +
                    "(@marcallanta12, @condicionllanta12, @rllanta12)", llanta12);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta13

        public static long SaveLlanta13(LlantaDerInt13 llanta13)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaDerInt13" +
                    "(marcallanta13, condicionllanta13, rllanta) values " +
                    "(@marcallanta13, @condicionllanta13, @rllanta13)", llanta13);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }

        //Llanta14


        public static long SaveLlanta14(LlantaDerExt14 llanta14)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaDerExt14" +
                    "(marcallanta14, condicionllanta14, rllanta) values " +
                    "(@marcallanta14, @condicionllanta14, @rllanta14)", llanta14);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta15
        public static long SaveLlanta15(LlantaIzqExt15 llanta15)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaIzqExt15" +
                    "(marcallanta15, condicionllanta15, rllanta) values " +
                    "(@marcallanta15, @condicionllanta15, @rllanta15)", llanta15);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta16
        public static long SaveLlanta16(LlantaIzqInt16 llanta16)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaIzqInt16" +
                    "(marcallanta16, condicionllanta16, rllanta) values " +
                    "(@marcallanta16, @condicionllanta16, @rllanta16)", llanta16);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta17
        public static long SaveLlanta17(LlantaDerInt17 llanta17)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaDerInt17" +
                    "(marcallanta17, condicionllanta17, rllanta) values " +
                    "(@marcallanta17, @condicionllanta17, @rllanta17)", llanta17);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }
        //Llanta18
        public static long SaveLlanta18(LlantaDerExt18 llanta18)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into LlantaDerExt18" +
                    "(marcallanta18, condicionllanta18, rllanta) values " +
                    "(@marcallanta18, @condicionllanta18, @rllanta18)", llanta18);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }

        //Llantas
        public static long SaveLlantas(Llantas llantas)
        {
            long id;

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into Llantas" +
                    "(idllanta11, idllanta12, idllanta13, idllanta14, idllanta15, idllanta16, idllanta17, idllanta18, rfoliosalida, rfolioentrada)" +
                    " values " + "(@idllanta11, @idllanta12, @idllanta13, @idllanta14, @idllanta15, @idllanta16, @idllanta17, @idllanta18, @rfoliosalida, @rfolioentrada)", llantas);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }

        
        public static long Insercion_Relacion_Llantas_Salida_Entrada(Llantas llantas)
        {
            long id;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                //crear base de datos desde 0.
                cnn.Execute("insert into Llantas" +
                    "(idllanta11, idllanta12, idllanta13, idllanta14, idllanta15, idllanta16, idllanta17, idllanta18, rfoliosalida, rfolioentrada)" +
                    " values " + "(@idllanta11, @idllanta12, @idllanta13, @idllanta14, @idllanta15, @idllanta16, @idllanta17, @idllanta18, @rfoliosalida, @rfolioentrada)", llantas);
                id = cnn.LastInsertRowId;
                transaction.Commit();
                return id;
            }
        }

        public static void Update_Generico(int folioinsertar, int idllanta11, int idllanta12, int idllanta13, int idllanta14, int idllanta15, int idllanta16,
            int idllanta17, int idllanta18)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaIzqExt11 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta11 = '" + idllanta11 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaIzqInt12 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta12 = '" + idllanta12 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaDerInt13 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta13 = '" + idllanta13 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaDerExt14 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta14 = '" + idllanta14 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaIzqExt15 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta15 = '" + idllanta15 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaIzqInt16 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta16 = '" + idllanta16 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaDerInt17 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta17 = '" + idllanta17 + "'");
                transaction.Commit();

            }

            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                SQLiteTransaction transaction = null;
                transaction = cnn.BeginTransaction();
                cnn.Execute("update LlantaDerExt18 " +
                    "set rllanta = '" + folioinsertar + "' where idllanta18 = '" + idllanta18 + "'");
                transaction.Commit();

            }
        }

        #endregion

        //Metodo para llenar el datatable que se esta usando como referencia en el reporte.
        public static void FillDataTableSalida(InspeccionReporte dataSet, string folioinspeccion)
        {
            dataSet.Tables[0].Rows.Clear();
            string query = "SELECT Inspeccion.folio,Inspeccion.foliosalida, InspeccionSalida.nocaja, InspeccionSalida.fecha, InspeccionSalida.hora, InspeccionSalida.cliente, InspeccionSalida.nombreoperador, " +
    "InspeccionSalida.temperaturaprogramada, InspeccionSalida.temperaturareal, InspeccionSalida.unidadmedida, " +
    "Imagenes.ImgCajaDerecha, Imagenes.ImgCajaFrontal, Imagenes.ImgCajaIzquierda, Imagenes.ImgCajaTrasera, Imagenes.ImgDiesel, Imagenes.ImgTermometro, " +
    "Luces.fderecha, Luces.cderecha, Luces.frderecha, Luces.dirderecha, Luces.plafon, Luces.fizquierda, Luces.cizquierda, Luces.frizquierda, Luces.dirizquierda," +
    "Luces.abs, Luces.observacionesluces, Perifericos.Manivela, Perifericos.LoderaIzq, Perifericos.LoderaDerecha, Perifericos.ManitasAire, Perifericos.RinesRemolque, Perifericos.ManguerasAire, " +
    "CompartimientosOcultos.Defensa, CompartimientosOcultos.Motor, CompartimientosOcultos.Llantas, CompartimientosOcultos.PisoCabina, CompartimientosOcultos.CilindrosAire, CompartimientosOcultos.Cambios," +
    "CompartimientosOcultos.QuintaRueda, CompartimientosOcultos.ExteriorChasis, CompartimientosOcultos.PisoRemolqueAdentro, CompartimientosOcultos.PuertasAdentroAfuera, CompartimientosOcultos.ParedesLados, " +
    "CompartimientosOcultos.TechoParedFrontal, CompartimientosOcultos.ParedFrontalPiso, CompartimientosOcultos.UnidadRefrigerada, CompartimientosOcultos.Mofle, CompartimientosOcultos.Fleje," +
    "LlantaIzqExt11.marcallanta11, LlantaIzqExt11.condicionllanta11, LlantaIzqInt12.marcallanta12, LlantaIzqInt12.condicionllanta12, LlantaDerInt13.marcallanta13, LlantaDerInt13.condicionllanta13, LlantaDerExt14.marcallanta14, LlantaDerExt14.condicionllanta14, LlantaIzqExt15.marcallanta15," +
    "LlantaIzqExt15.condicionllanta15, LlantaIzqInt16.marcallanta16, LlantaIzqInt16.condicionllanta16, LlantaDerInt17.marcallanta17, LlantaDerInt17.condicionllanta17, LlantaDerExt18.marcallanta18, LlantaDerExt18.condicionllanta18, Luces.fderecha, CompartimientosOcultos.TanqueCombustible, CompartimientosOcultos.Cabina, InspeccionSalida.inspector, '' as folioentrada" +
    " FROM Inspeccion " +
    " JOIN InspeccionSalida ON Inspeccion.foliosalida = InspeccionSalida.folio " +
    " JOIN Imagenes ON InspeccionSalida.idimagenes = Imagenes.idimagenes " +
    " JOIN Luces ON InspeccionSalida.idluces = Luces.idluces " +
    " JOIN Perifericos ON InspeccionSalida.idperifericos = Perifericos.idperifericos " +
    " JOIN CompartimientosOcultos ON InspeccionSalida.idcompartimientosocultos = CompartimientosOcultos.idcompartimientosocultos " +
    " JOIN Llantas ON InspeccionSalida.idllantas = Llantas.idllantas " +
    " JOIN LlantaIzqExt11 ON Llantas.idllanta11 = LlantaIzqExt11.idllanta11 " +
    " JOIN LlantaIzqInt12 ON Llantas.idllanta12 = LlantaIzqInt12.idllanta12 " +
    " JOIN LlantaDerInt13 ON Llantas.idllanta13 = LlantaDerInt13.idllanta13 " +
    " JOIN LlantaDerExt14 ON Llantas.idllanta14 = LlantaDerExt14.idllanta14 " +
    " JOIN LlantaIzqExt15 ON Llantas.idllanta15 = LlantaIzqExt15.idllanta15 " +
    " JOIN LlantaIzqInt16 ON Llantas.idllanta16 = LlantaIzqInt16.idllanta16 " +
    " JOIN LlantaDerInt17 ON Llantas.idllanta17 = LlantaDerInt17.idllanta17 " +
    " JOIN LlantaDerExt18 ON Llantas.idllanta18 = LlantaDerExt18.idllanta18 " +
    "WHERE Inspeccion.foliosalida = @foliosalida";
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@foliosalida", folioinspeccion);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                    da.Fill(dataSet.Tables[0]);
                }
                cnn.Close();
            }
        }
        public static void FillDataTableEntrada(InspeccionReporte dataSet, string folioinspeccion)
        {
            dataSet.Tables[0].Rows.Clear();
            string query = "SELECT Inspeccion.folio, '' as foliosalida, InspeccionEntrada.nocaja, InspeccionEntrada.fecha, InspeccionEntrada.hora, InspeccionEntrada.cliente, InspeccionEntrada.nombreoperador, " +
                " InspeccionEntrada.temperaturaprogramada, InspeccionEntrada.temperaturareal, InspeccionEntrada.unidadmedida, " +
                " Imagenes.ImgCajaDerecha, Imagenes.ImgCajaFrontal, Imagenes.ImgCajaIzquierda, Imagenes.ImgCajaTrasera, Imagenes.ImgDiesel, Imagenes.ImgTermometro, " +
                " Luces.fderecha, Luces.cderecha, Luces.frderecha, Luces.dirderecha, Luces.plafon, Luces.fizquierda, Luces.cizquierda, Luces.frizquierda, Luces.dirizquierda," +
                " Luces.abs, Luces.observacionesluces, Perifericos.Manivela, Perifericos.LoderaIzq, Perifericos.LoderaDerecha, Perifericos.ManitasAire, Perifericos.RinesRemolque, Perifericos.ManguerasAire, " +
                " CompartimientosOcultos.Defensa, CompartimientosOcultos.Motor, CompartimientosOcultos.Llantas, CompartimientosOcultos.PisoCabina, CompartimientosOcultos.CilindrosAire, CompartimientosOcultos.Cambios," +
                " CompartimientosOcultos.QuintaRueda, CompartimientosOcultos.ExteriorChasis, CompartimientosOcultos.PisoRemolqueAdentro, CompartimientosOcultos.PuertasAdentroAfuera, CompartimientosOcultos.ParedesLados, " +
                " CompartimientosOcultos.TechoParedFrontal, CompartimientosOcultos.ParedFrontalPiso, CompartimientosOcultos.UnidadRefrigerada, CompartimientosOcultos.Mofle, CompartimientosOcultos.Fleje," +
                " LlantaIzqExt11.marcallanta11, LlantaIzqExt11.condicionllanta11, LlantaIzqInt12.marcallanta12, LlantaIzqInt12.condicionllanta12, LlantaDerInt13.marcallanta13, LlantaDerInt13.condicionllanta13, LlantaDerExt14.marcallanta14, LlantaDerExt14.condicionllanta14, LlantaIzqExt15.marcallanta15," +
                " LlantaIzqExt15.condicionllanta15, LlantaIzqInt16.marcallanta16, LlantaIzqInt16.condicionllanta16, LlantaDerInt17.marcallanta17, LlantaDerInt17.condicionllanta17, LlantaDerExt18.marcallanta18, LlantaDerExt18.condicionllanta18, Luces.fderecha, CompartimientosOcultos.TanqueCombustible, CompartimientosOcultos.Cabina, InspeccionEntrada.inspector, Inspeccion.folioentrada" +
                " FROM Inspeccion" +
                " JOIN InspeccionEntrada ON Inspeccion.folioentrada = InspeccionEntrada.folio" +
                " JOIN Imagenes ON InspeccionEntrada.idimagenes = Imagenes.idimagenes" +
                " JOIN Luces ON InspeccionEntrada.idluces = Luces.idluces" +
                " JOIN Perifericos ON InspeccionEntrada.idperifericos = Perifericos.idperifericos" +
                " JOIN CompartimientosOcultos ON InspeccionEntrada.idcompartimientosocultos = CompartimientosOcultos.idcompartimientosocultos" +
                " JOIN Llantas ON InspeccionEntrada.idllantas = Llantas.idllantas" +
                " JOIN LlantaIzqExt11 ON Llantas.idllanta11 = LlantaIzqExt11.idllanta11" +
                " JOIN LlantaIzqInt12 ON Llantas.idllanta12 = LlantaIzqInt12.idllanta12" +
                " JOIN LlantaDerInt13 ON Llantas.idllanta13 = LlantaDerInt13.idllanta13" +
                " JOIN LlantaDerExt14 ON Llantas.idllanta14 = LlantaDerExt14.idllanta14" +
                " JOIN LlantaIzqExt15 ON Llantas.idllanta15 = LlantaIzqExt15.idllanta15" +
                " JOIN LlantaIzqInt16 ON Llantas.idllanta16 = LlantaIzqInt16.idllanta16" +
                " JOIN LlantaDerInt17 ON Llantas.idllanta17 = LlantaDerInt17.idllanta17" +
                " JOIN LlantaDerExt18 ON Llantas.idllanta18 = LlantaDerExt18.idllanta18" +
                " WHERE Inspeccion.folioentrada = @folioentrada";
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@folioentrada", folioinspeccion);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

                    da.Fill(dataSet.Tables[0]);
                }
                cnn.Close();
            }
        }
        //coneccion desde app.config
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
