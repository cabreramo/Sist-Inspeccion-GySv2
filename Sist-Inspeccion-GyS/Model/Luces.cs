using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS.Model
{
    public class Luces
    {
        private int _intIdLuces;

        public int idluces
        {
            get { return _intIdLuces; }
            set { _intIdLuces = value; }
        }
        private int _intFDerecha;

        public int fderecha
        {
            get { return _intFDerecha; }
            set { _intFDerecha = value; }
        }

        private int _intCDerecha;

        public int cderecha
        {
            get { return _intCDerecha; }
            set { _intCDerecha = value; }
        }

        private int _intFrDerecha;

        public int frderecha
        {
            get { return _intFrDerecha; }
            set { _intFrDerecha = value; }
        }

        private int _intDirDerecha;

        public int dirderecha
        {
            get { return _intDirDerecha; }
            set { _intDirDerecha = value; }
        }

        private int _intPlafon;

        public int plafon
        {
            get { return _intPlafon; }
            set { _intPlafon = value; }
        }

        private int _intFIzq;

        public int fizquierda
        {
            get { return _intFIzq; }
            set { _intFIzq = value; }
        }

        private int _intCIzq;

        public int cizquierda
        {
            get { return _intCIzq; }
            set { _intCIzq = value; }
        }

        private int _intFrIzq;

        public int frizquierda
        {
            get { return _intFrIzq; }
            set { _intFrIzq = value; }
        }

        private int _intDirIzq;

        public int dirizquierda
        {
            get { return _intDirIzq; }
            set { _intDirIzq = value; }
        }


        private int _intABS;

        public int abs
        {
            get { return _intABS; }
            set { _intABS = value; }
        }

        private string _strObservacionesLuces;

        public string observacionesluces
        {
            get { return _strObservacionesLuces; }
            set { _strObservacionesLuces = value; }
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
