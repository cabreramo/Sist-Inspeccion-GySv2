using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    class clsLlantas
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

        private string _strTipoInspeccion;

        public string TipoInspeccion
        {
            get { return _strTipoInspeccion; }
            set { _strTipoInspeccion = value; }
        }

        private string _strPosiciconLlantas;

        public string PosicionLlantas
        {
            get { return _strPosiciconLlantas; }
            set { _strPosiciconLlantas = value; }
        }

        private string _strCondicionLlanta;

        public string CondicionLlanta
        {
            get { return _strCondicionLlanta; }
            set { _strCondicionLlanta = value; }
        }

        private string _strMarcaLlanta;

        public string MarcaLlanta
        {
            get { return _strMarcaLlanta; }
            set { _strMarcaLlanta = value; }
        }


    }
}
