using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    class Caja
    {
        private string  _strPlacasCaja;

        public string  PlacasCaja
        {
            get { return _strPlacasCaja; }
            set { _strPlacasCaja = value; }
        }

        private DateTime  _dtmFecha;

        public DateTime Fecha
        {
            get { return _dtmFecha; }
            set { _dtmFecha = value; }
        }

        private string _strFolio;

        public string Folio
        {
            get { return _strFolio; }
            set { _strFolio = value; }
        }

        //aqui tengo duda si dejarlo con binario, o ponemos directamente la ruta con un string.

        private string _strImgCajaFrontal;

        public string ImgCajaFrontal
        {
            get { return _strImgCajaFrontal; }
            set { _strImgCajaFrontal = value; }
        }


        private string _strImgCajaDerecha
;

        public string ImgCajaDerecha

        {
            get { return _strImgCajaDerecha; }
            set { _strImgCajaDerecha = value; }
        }


        private string _strImgCajaIzquierda;

        public string ImgCajaIzquierda
        {
            get { return _strImgCajaIzquierda; }
            set { _strImgCajaIzquierda = value; }
        }


        private string _strImgCajaLateral;

        public string ImgCajaLateral
        {
            get { return _strImgCajaLateral; }
            set { _strImgCajaLateral = value; }
        }

        private string _strImgDiesel;

        public string ImgDiesel
        {
            get { return _strImgDiesel; }
            set { _strImgDiesel = value; }
        }

        private string _strImgTermometro;

        public string ImgTermometro
        {
            get { return _strImgTermometro; }
            set { _strImgTermometro = value; }
        }



        //

        private bool _boolCajaLimpia;

        public bool CajaLimpia
        {
            get { return _boolCajaLimpia; }
            set { _boolCajaLimpia = value; }
        }

        private bool _boolFDerecha;

        public bool FDerecha
        {
            get { return _boolFDerecha; }
            set { _boolFDerecha = value; }
        }

        private bool _boolCDerecha;

        public bool CDerecha
        {
            get { return _boolCDerecha; }
            set { _boolCDerecha = value; }
        }

        private bool _boolFRDerecha;

        public bool FRDerecha
        {
            get { return _boolFRDerecha; }
            set { _boolFRDerecha = value; }
        }

        private bool _boolDirDerecha;

        public bool DirDerecha
        {
            get { return _boolDirDerecha; }
            set { _boolDirDerecha = value; }
        }

        private bool  _boolPlafon;

        public bool  Plafon
        {
            get { return _boolPlafon; }
            set { _boolPlafon = value; }
        }

        private bool _boolFIzq;

        public bool FIzq
        {
            get { return _boolFIzq; }
            set { _boolFIzq = value; }
        }

        private bool _boolCIzq;

        public bool CIzq
        {
            get { return _boolCIzq; }
            set { _boolCIzq = value; }
        }

        private bool _boolFRIzq;

        public bool FRIzq
        {
            get { return _boolFRIzq; }
            set { _boolFRIzq = value; }
        }

        private bool _boolDirIzq;

        public bool DirIzq

        {
            get { return _boolDirIzq; }
            set { _boolDirIzq = value; }
        }


        private bool  _boolABS;

        public bool  ABS
        {
            get { return _boolABS; }
            set { _boolABS = value; }
        }

        private string  _strObservaciones;

        public string  Observaciones
        {
            get { return _strObservaciones; }
            set { _strObservaciones = value; }
        }













    }
}
