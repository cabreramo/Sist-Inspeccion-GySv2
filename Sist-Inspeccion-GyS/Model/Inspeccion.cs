using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    public class Inspeccion
    {
        #region LLave primaria.
        private string _strFolio;

        public string folio
        {
            get { return _strFolio; }
            set { _strFolio = value; }
        }

        private int _intNoCaja;

        public int nocaja
        {
            get { return _intNoCaja; }
            set {
                if (value.ToString() == "")
                {
                    throw new Exception("Ingrese el número de caja");
                }
                else
                {
                    _intNoCaja = value;
                }
            }
        }

        private string _strFecha;

        public string fecha
        {
            get { return _strFecha; }
            set {
                if (value == "")
                {
                    throw new Exception("Ingrese la fecha");
                }
                else
                {
                    _strFecha = value;
                }
            }
        }

        private string _strHora;

        public string hora
        {
            get { return _strHora; }
            set {
                if(value == "")
                {
                    throw new Exception("Ingrese la hora");
                }
                else
                {
                    _strHora = value;
                }
            }
        }

        #endregion



        private string _strCliente;

        public string cliente
        {
            get { return _strCliente; }
            set {
                if(value == "")
                {
                    throw new Exception("Ingrese el CIA");
                }
                else
                {
                    _strCliente = value;
                }
            }
        }


        private string _strNombreOperador;

        public string nombreoperador
        {
            get { return _strNombreOperador; }
            set {
                if (value == "")
                {
                    throw new Exception("Ingrese el nombre del operador");
                }
                else
                {
                    _strNombreOperador = value;
                }
            }
        }

        private string _strInspector;

        public string inspector
        {
            get { return _strInspector; }
            set {
                if(value=="")
                {
                    throw new Exception("Ingrese el nombre del inspector(a)");
                }
                else
                {
                    _strInspector = value;
                }
            }
        }

        private int _intIdImagenes;

        public int idimagenes
        {
            get { return _intIdImagenes; }
            set { _intIdImagenes = value; }
        }

        private int _intIdLuces;

        public int idluces
        {
            get { return _intIdLuces; }
            set { _intIdLuces = value; }
        }

        private int _intIdPerifericos;

        public int idperifericos
        {
            get { return _intIdPerifericos; }
            set { _intIdPerifericos = value; }
        }
        private int _intIdLlantas;

        public int idllantas
        {
            get { return _intIdLlantas; }
            set { _intIdLlantas = value; }
        }

        private int _intIdCompartimientosOCultos;

        public int idcompartimientosocultos
        {
            get { return _intIdCompartimientosOCultos; }
            set { _intIdCompartimientosOCultos = value; }
        }


        private string _strUnidadMedida;

        public string unidadmedida
        {
            get { return _strUnidadMedida; }
            set { _strUnidadMedida = value; }
        }

        private double _dblTemperaturaProgramada;

        public double temperaturaprogramada
        {
            get { return _dblTemperaturaProgramada; }
            set { _dblTemperaturaProgramada = value; }
        }

        private double _dblTemperaturaReal;

        public double temperaturareal
        {
            get { return _dblTemperaturaReal; }
            set { _dblTemperaturaReal = value; }
        }

        private string _strrfolioconsecutivo;

        public string folioconsecutivo
        {
            get { return _strrfolioconsecutivo; }
            set { _strrfolioconsecutivo = value; }
        }

    }
}
