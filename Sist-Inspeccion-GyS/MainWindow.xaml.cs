using Sist_Inspeccion_GyS.Views;
using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.Data;
using System.Configuration;


namespace Sist_Inspeccion_GyS
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnopenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnopenMenu.Visibility = Visibility.Collapsed;
            btncloseMenu.Visibility = Visibility.Visible;
        }

        private void btncloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnopenMenu.Visibility = Visibility.Visible;
            btncloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl userControl;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemRegistro":
                    userControl = new RegistroInspecciones();
                    GridMain.Children.Add(userControl);
                    break;
                case "ItemInspeccionesRealizadas":
                    userControl = new InspeccionesRealizadas();
                    GridMain.Children.Add(userControl);
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewMenu.SelectedItem = ItemRegistro;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
