using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    public class Perifericos
    {
        private int _intIdPerifericos;

        public int idperifericos
        {
            get { return _intIdPerifericos; }
            set { _intIdPerifericos = value; }
        }


        private int _boolManivela;

        public int Manivela
        {
            get { return _boolManivela; }
            set { _boolManivela = value; }
        }

        private int  _boolLoderaIzq;

        public int  LoderaIzq
        {
            get { return _boolLoderaIzq; }
            set { _boolLoderaIzq = value; }
        }

        private int _boolLoderaDer;

        public int LoderaDerecha
        {
            get { return _boolLoderaDer; }
            set { _boolLoderaDer = value; }
        }

        private int _boolManitasAire;

        public int ManitasAire
        {
            get { return _boolManitasAire; }
            set { _boolManitasAire = value; }
        }

        private int _boolRinesRemolque;

        public int RinesRemolque
        {
            get { return _boolRinesRemolque; }
            set { _boolRinesRemolque = value; }
        }

        private int _boolManguerasAire;

        public int ManguerasAire
        {
            get { return _boolManguerasAire; }
            set { _boolManguerasAire = value; }
        }

        private string _strrfoliosalida;

        public string rfoliosalida
        {
            get { return _strrfoliosalida; }
            set { _strrfoliosalida = value; }
        }

        private string _strrfolioentrada;

        public string rfolioentrada
        {
            get { return _strrfolioentrada; }
            set { _strrfolioentrada = value; }
        }



    }
}
