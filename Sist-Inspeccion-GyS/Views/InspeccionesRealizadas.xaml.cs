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

namespace Sist_Inspeccion_GyS.Views
{
    /// <summary>
    /// Lógica de interacción para InspeccionesRealizadas.xaml
    /// </summary>
    public partial class InspeccionesRealizadas : UserControl
    {
        public InspeccionesRealizadas()
        {
            InitializeComponent();

            List<Inspeccion> inspecciones = new List<Inspeccion>();
            inspecciones.Add(new Inspeccion() { placas = "A00-AAA", operador = "Test operador #1", cia = "Test cia #1", fechaSalida = new DateTime(1971, 7, 23), fechaEntrada = new DateTime(1991, 9, 2) });
            inspecciones.Add(new Inspeccion() { placas = "888-ABC", operador = "Test operador #2", cia = "Test cia #2", fechaSalida = new DateTime(1974, 1, 17), fechaEntrada = new DateTime(1991, 9, 2) });
            inspecciones.Add(new Inspeccion() { placas = "A01-AAA", operador = "Test operador #3", cia = "Test cia #3", fechaSalida = new DateTime(1991, 9, 2), fechaEntrada = new DateTime(1991, 9, 2) });

            dgInspecciones.ItemsSource = inspecciones;
        }
        public class Inspeccion
        {
            public string placas { get; set; }
            public string operador { get; set; }
            public string cia { get; set; }
            public DateTime fechaSalida { get; set; }
            public DateTime fechaEntrada { get; set; }

        }
    }
    
}
