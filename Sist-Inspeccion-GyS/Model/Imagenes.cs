using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS
{
    public class Imagenes
    {
        //private int _intIdImagenes;

        //public int idimagenes
        //{
        //    get { return _intIdImagenes; }
        //    set { _intIdImagenes = value; }
        //}
        private byte[] _strImgCajaFrontal;

        public byte[] ImgCajaFrontal
        {
            get { return _strImgCajaFrontal; }
            set { _strImgCajaFrontal = value; }
        }


        private byte[] _strImgCajaTrasera;

        public byte[] ImgCajaTrasera
        {
            get { return _strImgCajaTrasera; }
            set { _strImgCajaTrasera = value; }
        }



        private byte[] _strImgCajaDerecha
;

        public byte[] ImgCajaDerecha

        {
            get { return _strImgCajaDerecha; }
            set { _strImgCajaDerecha = value; }
        }


        private byte[] _strImgCajaIzquierda;

        public byte[] ImgCajaIzquierda
        {
            get { return _strImgCajaIzquierda; }
            set { _strImgCajaIzquierda = value; }
        }
       
        private byte[] _strImgDiesel;

        public byte[] ImgDiesel
        {
            get { return _strImgDiesel; }
            set { _strImgDiesel = value; }
        }

        private byte[] _strImgTermometro;

        public byte[] ImgTermometro
        {
            get { return _strImgTermometro; }
            set { _strImgTermometro = value; }
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
