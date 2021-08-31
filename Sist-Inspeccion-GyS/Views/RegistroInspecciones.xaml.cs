using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Sist_Inspeccion_GyS.Model;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using Sist_Inspeccion_GyS.Reporte;

namespace Sist_Inspeccion_GyS.Views
{
    /// <summary>
    /// Lógica de interacción para RegistroInspecciones.xaml
    /// </summary>
    /// 

    public partial class RegistroInspecciones : UserControl
    {
        bool Editar;
        InspeccionReporte dataset = new InspeccionReporte();
        public RegistroInspecciones()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Hora.SelectedTime = DateTime.Now;
                Fecha.SelectedDate = DateTime.Now;
            }, this.Dispatcher);

        }

        private void LoadInspeccionEntrada()
        {
            //entrada = SqliteDataAccess.LoadInspeccion();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inspeccion miInspeccion = new Inspeccion();
                if (rbEntrada.IsChecked == true)
                {

                }
                #region Captura de datos
                //Captura de datos
                #region Registro de Inf. General
                miInspeccion.inspector = txtInspector.Text;
                if (txtNoCaja.Text == "")
                { throw new Exception("Ingrese el número de caja"); }
                else
                { miInspeccion.nocaja = int.Parse(txtNoCaja.Text); }

                miInspeccion.nombreoperador = txtNombreOperador.Text;
                miInspeccion.cliente = txtCliente.Text;
                miInspeccion.fecha = Fecha.Text;
                miInspeccion.hora = Hora.Text;
                #endregion
                #region Registro de imagenes
                Imagenes misImagenes = new Imagenes();
                string pathdefault = System.IO.Path.GetFullPath("Resources/Logo.jpeg");
                byte[] imgDefault =  File.ReadAllBytes(pathdefault);
                if (imgDerecha.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgCajaDerecha = imgDefault;
                }
                else
                {
                    misImagenes.ImgCajaDerecha = File.ReadAllBytes(imgDerecha.Source.ToString().Substring(8));
                }
                if(imgFrontal.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgCajaFrontal = imgDefault;
                }
                else
                {
                    misImagenes.ImgCajaFrontal = File.ReadAllBytes(imgFrontal.Source.ToString().Substring(8));
                }
                if(imgTrasera.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgCajaTrasera = imgDefault;
                }
                else
                {
                    misImagenes.ImgCajaTrasera = File.ReadAllBytes(imgTrasera.Source.ToString().Substring(8));
                }
                if(imgDiesel.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgDiesel = imgDefault;
                }
                else
                {
                    misImagenes.ImgDiesel = File.ReadAllBytes(imgDiesel.Source.ToString().Substring(8));
                }
                if(imgIzquierda.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgCajaIzquierda = imgDefault;
                }
                else
                {
                    misImagenes.ImgCajaIzquierda = File.ReadAllBytes(imgIzquierda.Source.ToString().Substring(8));
                }
                if(imgTermometro.Source.ToString() == "pack://application:,,,/Resources/Logowbg.png")
                {
                    misImagenes.ImgTermometro = imgDefault;
                }
                else
                {
                    misImagenes.ImgTermometro = File.ReadAllBytes(imgTermometro.Source.ToString().Substring(8));
                }
                #endregion
                #region Registro de luces
                Luces miLuces = new Luces();
                miLuces.fderecha = Convert.ToInt32(tglFDerecha.IsChecked.Value);
                miLuces.cderecha = Convert.ToInt32(tglCDerecha.IsChecked.Value);
                miLuces.frderecha = Convert.ToInt32(tglFrDerecha.IsChecked.Value);
                miLuces.dirderecha = Convert.ToInt32(tglDirDerecha.IsChecked.Value);
                miLuces.plafon = Convert.ToInt32(tglPlafon.IsChecked.Value);
                miLuces.fizquierda = Convert.ToInt32(tglFIzquierda.IsChecked.Value);
                miLuces.cizquierda = Convert.ToInt32(tglCIzquierda.IsChecked.Value);
                miLuces.frizquierda = Convert.ToInt32(tglFrIzquierda.IsChecked.Value);
                miLuces.dirizquierda = Convert.ToInt32(tglDirIzquierda.IsChecked.Value);
                miLuces.abs = Convert.ToInt32(tglAbs.IsChecked.Value);
                miLuces.observacionesluces = txtObservaciones.Text;


                #endregion

                #region Registro de llantas
                Llantas misLlantas = new Llantas();

                //Registro de llanta 11
                LlantaIzqExt11 miLlanta11 = new LlantaIzqExt11();
                miLlanta11.marcallanta11 = cmbIzqExt11.Text;
                miLlanta11.condicionllanta11 = (lsbIzqExt11.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta11 = int.Parse(SqliteDataAccess.SaveLlanta11(miLlanta11).ToString());

                //Registro de llanta 12
                LlantaIzqInt12 miLlanta12 = new LlantaIzqInt12();
                miLlanta12.marcallanta12 = cmbIzqInt12.Text;
                miLlanta12.condicionllanta12 = (lsbIzqInt12.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta12 = int.Parse(SqliteDataAccess.SaveLlanta12(miLlanta12).ToString());



                //Registro de llanta 13
                LlantaDerInt13 miLlanta13 = new LlantaDerInt13();
                miLlanta13.marcallanta13 = cmbDerInt13.Text;
                miLlanta13.condicionllanta13 = (lsbDerInt13.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta13 = int.Parse(SqliteDataAccess.SaveLlanta13(miLlanta13).ToString());



                //Registro de llanta 14
                LlantaDerExt14 miLlanta14 = new LlantaDerExt14();
                miLlanta14.marcallanta14 = cmbDerExt14.Text;
                miLlanta14.condicionllanta14 = (lsbDerExt14.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta14 = int.Parse(SqliteDataAccess.SaveLlanta14(miLlanta14).ToString());

                //Registro de llanta 15
                LlantaIzqExt15 miLlanta15 = new LlantaIzqExt15();
                miLlanta15.marcallanta15 = cmbIzqExt15.Text;
                miLlanta15.condicionllanta15 = (lsbIzqExt15.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta15 = int.Parse(SqliteDataAccess.SaveLlanta15(miLlanta15).ToString());

                //Registro de llanta 16
                LlantaIzqInt16 miLlanta16 = new LlantaIzqInt16();
                miLlanta16.marcallanta16 = cmbIzqInt16.Text;
                miLlanta16.condicionllanta16 = (lsbIzqInt16.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta16 = int.Parse(SqliteDataAccess.SaveLlanta16(miLlanta16).ToString());

                //Registro de llanta 17
                LlantaDerInt17 miLlanta17 = new LlantaDerInt17();
                miLlanta17.marcallanta17 = cmbDerInt17.Text;
                miLlanta17.condicionllanta17 = (lsbDerInt17.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta17 = int.Parse(SqliteDataAccess.SaveLlanta17(miLlanta17).ToString());

                //Registro de llanta 18
                LlantaDerExt18 miLlanta18 = new LlantaDerExt18();
                miLlanta18.marcallanta18 = cmbDerExt18.Text;
                miLlanta18.condicionllanta18 = (lsbDerExt18.SelectedItem as ListBoxItem).Content.ToString();
                misLlantas.idllanta18 = int.Parse(SqliteDataAccess.SaveLlanta18(miLlanta18).ToString());


                #endregion

                #region Registro de compartimientos
                CompartimientosOcultos miCompartimiento = new CompartimientosOcultos();

                miCompartimiento.Defensa = Convert.ToInt32(tglDefensa.IsChecked.Value);
                miCompartimiento.Motor = Convert.ToInt32(tglMotor.IsChecked.Value);
                miCompartimiento.Llantas = Convert.ToInt32(tglLlantas.IsChecked.Value);
                miCompartimiento.PisoCabina = Convert.ToInt32(tglPisoCabina.IsChecked.Value);
                miCompartimiento.TanqueCombustible = Convert.ToInt32(tglTanqueCombustible.IsChecked.Value);
                miCompartimiento.Cabina = Convert.ToInt32(tglCabina.IsChecked.Value);
                miCompartimiento.CilindrosAire = Convert.ToInt32(tglCilindrosAire.IsChecked.Value);
                miCompartimiento.Cambios = Convert.ToInt32(tglCambios.IsChecked.Value);
                miCompartimiento.QuintaRueda = Convert.ToInt32(tglQuintaRueda.IsChecked.Value);
                miCompartimiento.ExteriorChasis = Convert.ToInt32(tglExteriorChasis.IsChecked.Value);
                miCompartimiento.PisoRemolqueAdentro = Convert.ToInt32(tglPisoRemolque.IsChecked.Value);
                miCompartimiento.PuertasAdentroAfuera = Convert.ToInt32(tglPuertas.IsChecked.Value);
                miCompartimiento.ParedesLados = Convert.ToInt32(tglParedesLados.IsChecked.Value);
                miCompartimiento.TechoParedFrontal = Convert.ToInt32(tglTechoParedFrontal.IsChecked.Value);
                miCompartimiento.ParedFrontalPiso = Convert.ToInt32(tglParedFrontalPiso.IsChecked.Value);
                miCompartimiento.UnidadRefrigerada = Convert.ToInt32(tglUnidadRefrigerda.IsChecked.Value);
                miCompartimiento.Mofle = Convert.ToInt32(tglMofle.IsChecked.Value);
                miCompartimiento.Fleje = Convert.ToInt32(tglSello.IsChecked.Value);

                #endregion

                #region Registro de perifericos
                Perifericos misPerifericos = new Perifericos();
                misPerifericos.Manivela = Convert.ToInt32(tglManivela.IsChecked.Value);
                misPerifericos.LoderaIzq = Convert.ToInt32(tglLoderaIzq.IsChecked.Value);
                misPerifericos.LoderaDerecha = Convert.ToInt32(tglLoderaDer.IsChecked.Value);
                misPerifericos.ManitasAire = Convert.ToInt32(tglManitasAire.IsChecked.Value);
                misPerifericos.RinesRemolque = Convert.ToInt32(tglRinesRemolque.IsChecked.Value);
                misPerifericos.ManguerasAire = Convert.ToInt32(tglManguerasAire.IsChecked.Value);

                #endregion

                #region Registro datos termómetro
                if (txtTProgramada.Text != "") { miInspeccion.temperaturaprogramada = double.Parse(txtTProgramada.Text.ToString()); }
                if (txtTReal.Text != "") { miInspeccion.temperaturareal = double.Parse(txtTReal.Text.ToString()); }
                if (TermoF.IsSelected) { miInspeccion.unidadmedida = "Farenheit"; }
                else if (TermoC.IsSelected) { miInspeccion.unidadmedida = "Centigrados"; }
                #endregion
                #endregion

                if (rbEntrada.IsChecked == true)
                {
                    miInspeccion.folio = miInspeccion.nocaja + miInspeccion.fecha.Replace("/", "").ToString() + miInspeccion.hora.Replace(":", "").ToString() + "E";

                    //Busqueda de la salida mas reciente de la caja
                    string foliorecientesalida = SqliteDataAccess.BusquedaSalidaCaja(miInspeccion.nocaja);



                    #region Indicar folio a llaves foraneas e insertar componentes
                    #region RelacionImagenesSalidaEntrada
                    //imagenes. rfolioentrada = folioentrada
                    misImagenes.rfolioentrada = miInspeccion.folio;
                    //metodo para que se inserte el rfolioentrada en la relacion con rfoliosalida
                    miInspeccion.idimagenes = (int)SqliteDataAccess.SaveImagenes(misImagenes);
                    #endregion


                    //luces.rfolioentrada = folio entrada
                    miLuces.rfolioentrada = miInspeccion.folio;
                    miInspeccion.idluces = (int)SqliteDataAccess.SaveLuces(miLuces);

                    //compartimiento.rfolioentrada = folio entrada
                    miCompartimiento.rfolioentrada = miInspeccion.folio;
                    miInspeccion.idcompartimientosocultos = (int)SqliteDataAccess.SaveCompartimientos(miCompartimiento);

                    //perifericos.rfolioentrada = folioentrada
                    misPerifericos.rfolioentrada = miInspeccion.folio;
                    miInspeccion.idperifericos = (int)SqliteDataAccess.SavePerifericos(misPerifericos);


                    //llantas.rfolioentrada = folioentrada

                    misLlantas.rfolioentrada = miInspeccion.folio;
                    miInspeccion.idllantas = (int)SqliteDataAccess.SaveLlantas(misLlantas);


                    miLlanta11.rllanta11 = miInspeccion.idllantas.ToString();
                    miLlanta12.rllanta12 = miInspeccion.idllantas.ToString();
                    miLlanta13.rllanta13 = miInspeccion.idllantas.ToString();
                    miLlanta14.rllanta14 = miInspeccion.idllantas.ToString();
                    miLlanta15.rllanta15 = miInspeccion.idllantas.ToString();
                    miLlanta16.rllanta16 = miInspeccion.idllantas.ToString();
                    miLlanta17.rllanta17 = miInspeccion.idllantas.ToString();
                    miLlanta18.rllanta18 = miInspeccion.idllantas.ToString();

                    //metodo para realizar el update en llantas.

                    SqliteDataAccess.Update_Generico(miInspeccion.idllantas, misLlantas.idllanta11, misLlantas.idllanta12,
                        misLlantas.idllanta13, misLlantas.idllanta14, misLlantas.idllanta15, misLlantas.idllanta16,
                        misLlantas.idllanta17, misLlantas.idllanta18);
                    #endregion


                    if (SqliteDataAccess.SaveInspeccionEntrada(miInspeccion) > 0)
                    {
                        //Busqueda de la relación correspondiente
                        int foliorelacion = SqliteDataAccess.BusquedaFolioRelacion(foliorecientesalida);

                        //Verificar que la relación no tiene entrada
                        if (SqliteDataAccess.VerificarQueHaySalida(foliorelacion))
                        {
                            //Insertar en relación y verificar que se inserto correctamente
                            if (SqliteDataAccess.InsercionRelacion(foliorelacion, miInspeccion.folio) > 0)
                            {
                                if (Editar)
                                {
                                    Editar = false;
                                }
                                SqliteDataAccess.Update_InspeccionR_Entrada(foliorelacion, miInspeccion.folio);
                                btnGuardar.IsEnabled = false;
                                btnAbrirReporte.IsEnabled = true;
                                btnEditar.IsEnabled = true;
                                HabilitacionControles(false);

                                //Llenar el DataTable usado en el reporte
                                SqliteDataAccess.FillDataTableEntrada(dataset, miInspeccion.folio);
                                //Crear una instancia del reporte
                                rptInspeccion rpt = new rptInspeccion();
                                //Cargar el reporte y darle el dataset con los datos actualizados
                                rpt.Load(@"\\Reporte\\rptInspeccion.rpt");
                                rpt.SetDataSource(dataset);
                                rpt.Refresh();
                                //Revisar si existe la carpeta del mes correspondiente, sino existe, se crea.
                                string carpetames = miInspeccion.fecha.Substring(3).Replace("/", "-").ToString();
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                //Exportar el reporte como pdf a la ubicación indicada
                                rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.IO.Path.Combine(path + foliorelacion + "-" + miInspeccion.folio + ".pdf"));
                                MessageBox.Show("Inspección guardada correctamente");

                            }
                        }
                        else
                        {
                            SqliteDataAccess.DeleteInspeccionEntrada(miInspeccion.folio);
                            throw new Exception("El número de remolque ingresado no ha tenido salida, verifique los datos");
                        }
                    }
                    else
                    {
                        throw new Exception("Error al guardar la inspección");
                    }
                }
                else
                if (rbSalida.IsChecked == true)
                { 
                    miInspeccion.folio = miInspeccion.nocaja + miInspeccion.fecha.Replace("/", "").ToString() + miInspeccion.hora.Replace(":", "").ToString() + "S";

                    #region Indicar folio a llaves foraneas e insertar componentes
                    //se inserta el rfoliosalida de imagenes
                    misImagenes.rfoliosalida = miInspeccion.folio;
                    miInspeccion.idimagenes = int.Parse(SqliteDataAccess.SaveImagenes(misImagenes).ToString());

                    //se inserta el rfoliosalida de luces
                    miLuces.rfoliosalida = miInspeccion.folio;
                    miInspeccion.idluces = int.Parse(SqliteDataAccess.SaveLuces(miLuces).ToString());

                    //se inserta el rfoliosalida de compartimientos
                    miCompartimiento.rfoliosalida = miInspeccion.folio;
                    miInspeccion.idcompartimientosocultos = int.Parse(SqliteDataAccess.SaveCompartimientos(miCompartimiento).ToString());


                    //se inserta el rfoliosalida de Perifericos.
                    misPerifericos.rfoliosalida = miInspeccion.folio;
                    miInspeccion.idperifericos = int.Parse(SqliteDataAccess.SavePerifericos(misPerifericos).ToString());


                    //se inserta el rfoliosalida de llantas
                    misLlantas.rfoliosalida = miInspeccion.folio;
                    miInspeccion.idllantas = int.Parse(SqliteDataAccess.SaveLlantas(misLlantas).ToString());


                    miLlanta11.rllanta11 = miInspeccion.idllantas.ToString();
                    miLlanta12.rllanta12 = miInspeccion.idllantas.ToString();
                    miLlanta13.rllanta13 = miInspeccion.idllantas.ToString();
                    miLlanta14.rllanta14 = miInspeccion.idllantas.ToString();
                    miLlanta15.rllanta15 = miInspeccion.idllantas.ToString();
                    miLlanta16.rllanta16 = miInspeccion.idllantas.ToString();
                    miLlanta17.rllanta17 = miInspeccion.idllantas.ToString();
                    miLlanta18.rllanta18 = miInspeccion.idllantas.ToString();

                    //metodo para realizar el update en llantas.

                    SqliteDataAccess.Update_Generico(miInspeccion.idllantas, misLlantas.idllanta11, misLlantas.idllanta12,
                                    misLlantas.idllanta13, misLlantas.idllanta14, misLlantas.idllanta15, misLlantas.idllanta16,
                                    misLlantas.idllanta17, misLlantas.idllanta18);

                    #endregion

                    //busco el folio entrada con ese numero de caja.
                    //Agregar validacion si no hay un folio con esa entrada, que siga el flujo.


                    //Verificar que ha tenido salidas anteriores
                    if (SqliteDataAccess.TieneSalida(miInspeccion.nocaja))
                    {
                        //Si, si tiene salidas anteriores, se busca la ultima salida, su folio relacion y si no tuvo entrada, se lanza una excepción.
                        string ultimasalidacaja = SqliteDataAccess.BusquedaSalidaCaja(miInspeccion.nocaja);
                        int foliorelacionultimasalida = SqliteDataAccess.BusquedaFolioRelacion(ultimasalidacaja);
                        if (SqliteDataAccess.VerificarQueNoHayEntrada(foliorelacionultimasalida))
                        {
                            throw new Exception("El número de remolque ingresado no ha tenido entrada, verifique los datos");
                            //string folioentrada = SqliteDataAccess.BusquedaEntradaCaja(miInspeccion.nocaja);
                            ////Busqueda de la relación correspondiente
                            //int foliorelacionentradasalida = SqliteDataAccess.BusquedaFolioEntrada(folioentrada);
                            ////Verificar que la relación no tiene entrada
                            //if (SqliteDataAccess.VerificarQueHaySalida(foliorelacionentradasalida))
                            //{
                            //    throw new Exception("El número de remolque ingresado no ha tenido entrada, verifique los datos");
                            //}
                        }
                    }
                    //Si no ha tenido salidas anteriores, se guarda normalmente.
                    if (SqliteDataAccess.SaveInspeccionSalida(miInspeccion) > 0)
                    {
                        InspeccionRelacion inspeccionRelacion = new InspeccionRelacion();
                        inspeccionRelacion.foliosalida = miInspeccion.folio;
                        if (Editar && SqliteDataAccess.RevisarSiFolioRelacionVacio() || SqliteDataAccess.RevisarSiFolioRelacionVacio())
                        {
                            int folioconsecutivoeditar = SqliteDataAccess.Ultimo_FolioRelacion_Insertado();
                            //SqliteDataAccess.SaveInspeccionSalida(miInspeccion);
                            //metodo que inserta en el consecutivo dentro de la tabla inspeccion relacion.
                            SqliteDataAccess.EditarSalida(folioconsecutivoeditar, inspeccionRelacion.foliosalida);

                            //MessageBox.Show("Edicion correcta", "EDITAR");
                            Editar = false;

                        }
                        else
                        {
                            //SqliteDataAccess.SaveInspeccionRelacion(inspeccionRelacion.foliosalida);
                            //Insertar en relación y verificar que se inserto correctamente
                            if (SqliteDataAccess.SaveInspeccionRelacion(inspeccionRelacion) <= 0)
                            {
                                throw new Exception("Error al guardar");
                            }
                        }
                        ////se busca el folio
                        //string rfolios = SqliteDataAccess.BusquedaSalidaCaja(miInspeccion.nocaja);
                        ////se busca el folio de inspeccion relacion atraves de la salida.
                        int foliorelacion = SqliteDataAccess.Ultimo_FolioRelacion_Insertado();
                        //int folioeditar = (int)SqliteDataAccess.BusquedaFolioRelacion(rfolios);
                        ////se inserta el folio que relaciona inspeccionr con inspeccionsalida
                        SqliteDataAccess.Update_InspeccionR_Salida(foliorelacion, miInspeccion.folio);

                        btnGuardar.IsEnabled = false;
                        btnAbrirReporte.IsEnabled = true;
                        btnEditar.IsEnabled = true;
                        HabilitacionControles(false);

                        //Llenar el DataTable usado en el reporte
                        SqliteDataAccess.FillDataTableSalida(dataset, inspeccionRelacion.foliosalida);
                        //Crear una instancia del reporte
                        rptInspeccion rpt = new rptInspeccion();
                        //Cargar el reporte y darle el dataset con los datos actualizados
                        rpt.Load(@"\\Reporte\\rptInspeccion.rpt");
                        rpt.SetDataSource(dataset);
                        rpt.Refresh();
                        //Revisar si existe la carpeta del mes correspondiente, sino existe, se crea.
                        string carpetames = miInspeccion.fecha.Substring(3).Replace("/", "-").ToString();
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        //Exportar el reporte como pdf a la ubicación indicada
                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, System.IO.Path.Combine(path + foliorelacion + "-" + miInspeccion.folio + ".pdf"));
                        MessageBox.Show("Inspección guardada correctamente");
                    }
                    else
                    {
                        throw new Exception("Error al guardar la inspección");
                    }
                }
                else
                {
                    throw new Exception("Seleccione un tipo de inspección");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en la insercion", MessageBoxButton.OK);

            }
        }

        #region Botones y métodos Imagenes
        private void btnSubirImgFrontal_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgFrontal);
        }

        private void btnSubirImgTrasera_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgTrasera);
        }

        private void btnSubirImgDiesel_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgDiesel);
        }

        private void btnSubirImgIzquierda_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgIzquierda);
        }

        private void btnSubirImgTermometro_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgTermometro);
        }

        private void btnSubirImgDerecha_Click(object sender, RoutedEventArgs e)
        {
            SubirImagen(imgDerecha);
        }
        //Método genérico para subir imagenes de la caja.
        void SubirImagen(System.Windows.Controls.Image imagen )
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Seleccione una imagen";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            fileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == true)
            {
                //FileStream FilStr = new FileStream(fileDialog.FileName, FileMode.Open);
                //BinaryReader binaryReader = new BinaryReader(FilStr);
                //imagenb = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
                //FilStr.Close();
                //binaryReader.Close();
                imagen.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnExpandirTermometro_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgTermometro, "Imagen termómetro");
        }

        private void btnExpandirImgDiesel_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgDiesel, "Imagen diesel");
        }

        private void btnExpandirImgIzquierda_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgIzquierda, "Imagen parte izquierda");
        }

        private void btnExpandirImgTrasera_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgTrasera, "Imagen parte trasera");
        }

        private void btnExpandirImgDerecha_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgDerecha, "Imagen parte derecha");
        }

        private void btnExpandirImgFrontal_Click(object sender, RoutedEventArgs e)
        {
            ExpandirImagen(imgFrontal, "Imagen parte frontal");
        }

        private void btnPopUpCerrar_Click(object sender, RoutedEventArgs e)
        {
            PopUp.IsOpen = false;
        }

        //Método genérico para expandir imagenes
        void ExpandirImagen(System.Windows.Controls.Image imagen, string PopupTitulo)
        {
            txtPopUpTitulo.Text = PopupTitulo;
            PopUpImg.Source = imagen.Source;
            PopUp.IsOpen = true;
        }
        #endregion
        private void txtNoCaja_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Encender bandera 
                Editar = true;
                int folioconsecutivoeditar;
                //buscar folio consecutivo en base al ultimo insertado en inspeccion relacion.
                folioconsecutivoeditar = SqliteDataAccess.Ultimo_FolioRelacion_Insertado();
                if (rbSalida.IsChecked == true)
                {
                    // select que regrese folio salida.
                    string foliosalida = SqliteDataAccess.Buscar_FolioSalida_Editar(folioconsecutivoeditar);//metodo para buscarlo.

                    //Se busca el mes de la carpeta para el path
                    string carpetames = SqliteDataAccess.EncontrarFechaInspeccion(foliosalida, "salida").Substring(3).Replace("/", "-").ToString(); ;

                    //metodo para eliminar el folio de inspeccion.
                    SqliteDataAccess.DeleteInspeccionSalidaRelacion(foliosalida);

                    //se manda el folio salida para realizar un update en la tabla de relación
                    SqliteDataAccess.Update_Inspeccion_Salida(folioconsecutivoeditar);

                    //Formación del path y eliminación del reporte que será editado
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                    string fullpath = System.IO.Path.Combine(path + folioconsecutivoeditar + "-" + foliosalida + ".pdf");
                    EliminarPDF(fullpath);

                }
                else if (rbEntrada.IsChecked == true)
                {
                    //Buscar folioentrada que será eliminado
                    string folioentrada = SqliteDataAccess.Buscar_FolioEntrada_Editar(folioconsecutivoeditar);
                    //Se busca el mes de la carpeta para el path
                    string carpetames = SqliteDataAccess.EncontrarFechaInspeccion(folioentrada, "entrada").Substring(3).Replace("/", "-").ToString();
                    //metodo para eliminar el folio de inspeccion.
                    SqliteDataAccess.DeleteInspeccionEntrada(folioentrada);

                    //Eliminar el folioentrada en la tabla relación indicando el folioconsecutivo donde habia sido insertado
                    SqliteDataAccess.Update_Inspeccion_Entrada(folioconsecutivoeditar);

                    //Formación del path y eliminación del reporte que será editado
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                    string fullpath = System.IO.Path.Combine(path + folioconsecutivoeditar + "-" + folioentrada + ".pdf");
                    EliminarPDF(fullpath);
                }
                btnGuardar.IsEnabled = true;
                btnAbrirReporte.IsEnabled = false;
                btnEditar.IsEnabled = false;
                HabilitacionControles(true);
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Error al editar");
            }
        }

        void HabilitacionControles(bool banderaHabilitar)
        {
            //Habilitar los controles
            if(banderaHabilitar)
            {
                grbTipoInspeccion.IsEnabled = true;
                grbInfoGeneral.IsEnabled = true;
                grbImagenes.IsEnabled = true;
                grbLuces.IsEnabled = true;
                grbLlantas.IsEnabled = true;
                grbPerifericos.IsEnabled = true;
                grbTermometro.IsEnabled = true;
                grbCompartimientos.IsEnabled = true;
            }
            //Deshabilitar los controles
            else if(!banderaHabilitar)
            {
                grbTipoInspeccion.IsEnabled = false;
                grbInfoGeneral.IsEnabled = false;
                grbImagenes.IsEnabled = false;
                grbLuces.IsEnabled = false;
                grbLlantas.IsEnabled = false;
                grbPerifericos.IsEnabled = false;
                grbTermometro.IsEnabled = false;
                grbCompartimientos.IsEnabled = false;
            }
        }

        void Limpiar(Visual myMainWindow)
        {
            //Content = new RegistroInspecciones();

            int childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
            for(int i = 0; i<childrenCount;i++)
            {
                var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
                if (visualChild is TextBox)
                {
                    TextBox tb = (TextBox)visualChild;

                    tb.Clear();
                }
                else if(visualChild is ToggleButton)
                {
                    ToggleButton tb = (ToggleButton)visualChild;
                    tb.IsChecked = false;
                }
                else if (visualChild is Image)
                {
                    Image Img = (Image)visualChild;
                    Img.Source = new BitmapImage(new Uri("/Resources/Logowbg.png",UriKind.Relative));
                }
                else if(visualChild is ComboBox)
                {
                    ComboBox cmb = (ComboBox)visualChild;
                    cmb.SelectedIndex = 0;
                }
                Limpiar(visualChild);
            }
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            btnGuardar.IsEnabled = true;
            Limpiar(grbInfoGeneral);
            Limpiar(grbImagenes);
            Limpiar(grbLuces);
            Limpiar(grbLlantas);
            Limpiar(grbPerifericos);
            Limpiar(grbTermometro);
            Limpiar(grbCompartimientos);
            unidadMedidaTermometro.SelectedIndex = 0;
            lsbIzqExt11.SelectedIndex = 0;
            lsbIzqInt12.SelectedIndex = 0;
            lsbDerInt13.SelectedIndex = 0;
            lsbDerExt14.SelectedIndex = 0;
            lsbIzqExt15.SelectedIndex = 0;
            lsbIzqInt16.SelectedIndex = 0;
            lsbDerInt17.SelectedIndex = 0;
            lsbDerExt18.SelectedIndex = 0;


            btnAbrirReporte.IsEnabled = false;
            btnEditar.IsEnabled = false;
            HabilitacionControles(true);
        }

        void EliminarPDF(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Error al eliminar el reporte");
            }
        }

        private void btnAbrirReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int folioconsecutivoeditar;
                //buscar folio consecutivo en base al ultimo insertado en inspeccion relacion.
                folioconsecutivoeditar = SqliteDataAccess.Ultimo_FolioRelacion_Insertado();
                if (rbSalida.IsChecked == true)
                {
                    // select que regrese folio salida.
                    string foliosalida = SqliteDataAccess.Buscar_FolioSalida_Editar(folioconsecutivoeditar);//metodo para buscarlo.

                    //Se busca el mes de la carpeta para el path
                    string carpetames = SqliteDataAccess.EncontrarFechaInspeccion(foliosalida, "salida").Substring(3).Replace("/", "-").ToString(); ;

                    //Formación del path y eliminación del reporte que será editado
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                    string fullpath = System.IO.Path.Combine(path + folioconsecutivoeditar + "-" + foliosalida + ".pdf");
                    AbrirPDF(fullpath);
                }
                else if (rbEntrada.IsChecked == true)
                {
                    //Buscar folioentrada que será eliminado
                    string folioentrada = SqliteDataAccess.Ultimo_FolioEntrada();
                    //Se busca el mes de la carpeta para el path
                    string carpetames = SqliteDataAccess.EncontrarFechaInspeccion(folioentrada, "entrada").Substring(3).Replace("/", "-").ToString();

                    //Formación del path y eliminación del reporte que será editado
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\";
                    string fullpath = System.IO.Path.Combine(path + folioconsecutivoeditar + "-" + folioentrada + ".pdf");
                    AbrirPDF(fullpath);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error al abrir el reporte");
            }
        }

        void AbrirPDF(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = path;
                    process.Start();
                }
                else
                {
                    throw new Exception("El pdf seleccionado no existe");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error al abrir el pdf");
            }
        }

        private void txtTProgramada_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private static readonly Regex _regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
        //private static readonly Regex _regex1 = new Regex("[^'\s/]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void txtTProgramada_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void txtTReal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }
    }
}
