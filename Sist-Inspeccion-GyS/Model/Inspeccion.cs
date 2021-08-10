using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    class Inspeccion
    {
        private int _intFolio;

        public int Folio
        {
            get { return _intFolio; }
            set { _intFolio = value; }
        }

        private DateTime _dtmFecha;

        public DateTime Fecha
        {
            get { return _dtmFecha; }
            set { _dtmFecha = value; }
        }


        private string _strPlacasCaja;

        public string PlacasCaja
        {
            get { return _strPlacasCaja; }
            set { _strPlacasCaja = value; }
        }

        private string _strPlacasTractor;

        public string PlacasTractor
        {
            get { return _strPlacasTractor; }
            set { _strPlacasTractor = value; }
        }

        private string _strNombreOperador;

        public string NombreOperador
        {
            get { return _strNombreOperador; }
            set { _strNombreOperador = value; }
        }

        private string _strCliente;

        public string Cliente
        {
            get { return _strCliente; }
            set { _strCliente = value; }
        }

        //Datos que se agregaron despues de la junta.

        private string _strInspector;

        public string Inspector
        {
            get { return _strInspector; }
            set { _strInspector = value; }
        }




        private string _strUnidadMedida;

        public string UnidadMedida
        {
            get { return _strUnidadMedida; }
            set { _strUnidadMedida = value; }
        }

        private double _dblTemperatura;

        public double Temperatura
        {
            get { return _dblTemperatura; }
            set { _dblTemperatura = value; }
        }









    }
}
