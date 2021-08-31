using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sist_Inspeccion_GyS.Model
{
    public class DataGridValues:INotifyPropertyChanged
    {
        public DataGridValues()
        {
            seleccionado = false;
        }
        private int _intfolio;

        public int folio
        {
            get { return _intfolio; }
            set { _intfolio = value; }
        }

        private string _strfolioSalida;

        public string foliosalida
        {
            get { return _strfolioSalida; }
            set { _strfolioSalida = value; }
        }
        private string _strfolioEntrada;

        public string folioentrada
        {
            get { return _strfolioEntrada; }
            set { _strfolioEntrada = value; }
        }
        private bool _blnSeleccionado;

        public bool seleccionado
        {
            get { return _blnSeleccionado; }
            set { 
                _blnSeleccionado = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
