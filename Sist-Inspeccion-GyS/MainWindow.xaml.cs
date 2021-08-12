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

namespace Sist_Inspeccion_GyS
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string connetionString;
        //SQLiteConnection connection;
        //SQLiteDataAdapter adapter;
        //DataSet ds = new DataSet();
        //string Sql;



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
                case "ItemInicio":
                    userControl = new Inicio();
                    GridMain.Children.Add(userControl);
                    break;
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
            ListViewMenu.SelectedItem = ItemInicio;

            //cadena de conexion sql server usano la autenticacion de windows.
            //connetionString = "Data Source=;Version=3;New=True;Compress=True;";
            //connection = new SQLiteConnection(connetionString);
            //Sql = "select * from sistemainspeccion";
            //try
            //{
            //    connection.Open();
            //    adapter = new SQLiteDataAdapter(Sql, connection);
            //    adapter.Fill(ds);
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    connection.Close();
            //}
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
