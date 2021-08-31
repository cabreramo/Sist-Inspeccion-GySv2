using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS.Model
{
    public class InspeccionRelacion
    {
        private int _intfolio;

        public int folio
        {
            get { return _intfolio; }
            set { _intfolio = value; }
        }

        private string _strFolioEntrada;

        public string folioentrada
        {
            get { return _strFolioEntrada; }
            set { _strFolioEntrada = value; }
        }

        private string _strFolioSalida;

        public string foliosalida
        {
            get { return _strFolioSalida; }
            set { _strFolioSalida = value; }
        }


    }
}
