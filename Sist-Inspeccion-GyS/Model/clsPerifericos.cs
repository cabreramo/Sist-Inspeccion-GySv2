using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    class clsPerifericos
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

        private bool _boolManivela;

        public bool Manivela
        {
            get { return _boolManivela; }
            set { _boolManivela = value; }
        }

        private bool  _boolLoderaIzq;

        public bool  LoderaIzq
        {
            get { return _boolLoderaIzq; }
            set { _boolLoderaIzq = value; }
        }

        private bool _boolLoderaDer;

        public bool LoderaDerecha
        {
            get { return _boolLoderaDer; }
            set { _boolLoderaDer = value; }
        }

        private bool _boolManitasAire;

        public bool ManitasAire
        {
            get { return _boolManitasAire; }
            set { _boolManitasAire = value; }
        }

        private bool _boolRinesRemolque;

        public bool RinesRemolque
        {
            get { return _boolRinesRemolque; }
            set { _boolRinesRemolque = value; }
        }

        private bool _boolManguerasAire;

        public bool ManguerasAire
        {
            get { return _boolManguerasAire; }
            set { _boolManguerasAire = value; }
        }



    }
}
