using Sist_Inspeccion_GyS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sist_Inspeccion_GyS.Views
{
    /// <summary>
    /// Lógica de interacción para InspeccionesRealizadas.xaml
    /// </summary>
    public partial class InspeccionesRealizadas : UserControl
    {
        private ObservableCollection<DataGridValues> lstInspeccionesRealizadas;
        public InspeccionesRealizadas()
        {
            InitializeComponent();
        }


        private void usrCInspeccionesRealizadas_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizarDataGrid();
        }
        private void ActualizarDataGrid()
        {
            lstInspeccionesRealizadas = new ObservableCollection<DataGridValues>(SqliteDataAccess.LoadInspecciones());
            dgInspecciones.ItemsSource = lstInspeccionesRealizadas;
        }

        private void btnEliminarSeleccionados_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta seguro de que desea eliminar las inspecciones seleccionadas?, Esta acción sera irreversible", "Atención!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (DataGridValues item in dgInspecciones.ItemsSource)
                    {
                        if (((CheckBox)chkSeleccionada.GetCellContent(item)).IsChecked == true)
                        {
                            SqliteDataAccess.DeleteInspeccionRelacion(item.folio);
                            //EliminarPDF(item.foliosalida, "salida");
                            //if(item.folioentrada != "") { EliminarPDF(item.folioentrada, "entrada"); }
                        }
                    }
                    ActualizarDataGrid();
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Error al eliminar");
            }
        }

        private void btnAbrirPDFSalida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string foliosalida = ((DataGridValues)dgInspecciones.CurrentItem).foliosalida;
                AbrirPDF(foliosalida, "salida");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Error al abrir el pdf");
            }
        }

        private void btnAbrirPDFEntrada_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folioentrada = ((DataGridValues)dgInspecciones.CurrentItem).folioentrada;
                AbrirPDF(folioentrada, "entrada");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error al abrir el pdf");
            }
        }
        void AbrirPDF(string folio, string tipo)
        {
            try
            {
                string fecha = SqliteDataAccess.EncontrarFechaInspeccion(folio.Substring(folio.IndexOf("-") + 1), tipo);
                string carpetames = fecha.Substring(3).Replace("/", "-").ToString();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\" + folio + ".pdf";
                if (File.Exists(path))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = path;
                    process.Start();
                }
                else
                {
                    throw new Exception("El pdf seleccionado no existe");
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error al abrir el pdf");
            }
        }
        void EliminarPDF(string folio, string tipo)
        {
            try
            {
                string fecha = SqliteDataAccess.EncontrarFechaInspeccion(folio.Substring(folio.IndexOf("-") + 1), tipo);
                string carpetames = fecha.Substring(3).Replace("/", "-").ToString();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Reportes de inspeccion\\" + carpetames + "\\" + folio + ".pdf";
                if(File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message, "Error al eliminar el pdf");
            }
        }

        private void btnEliminarInspecciones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string foliosalida = ((DataGridValues)dgInspecciones.CurrentItem).foliosalida;
                string folioentrada = ((DataGridValues)dgInspecciones.CurrentItem).folioentrada;
                int folio = ((DataGridValues)dgInspecciones.CurrentItem).folio;
                SqliteDataAccess.DeleteInspeccionRelacion(folio);
                ActualizarDataGrid();
                
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Error al eliminar");
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstInspeccionesRealizadas = new ObservableCollection<DataGridValues>(SqliteDataAccess.BuscarInspecciones(txtBuscar.Text));
            dgInspecciones.ItemsSource = lstInspeccionesRealizadas;
        }
    }
    
}
