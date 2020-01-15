using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace WpfApp2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tema6Entities contexto;
        private CLIENTE cliente;

        public MainWindow()
        {
            InitializeComponent();

            contexto = new Tema6Entities();

            //contexto.CLIENTES.Load();

            //clientesListBox.DataContext = contexto.CLIENTES.Local;

            var consulta = from n in contexto.CLIENTES.Include("PEDIDOS") select n;

            clientesListBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            modificarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            eliminarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());

            cliente = new CLIENTE();
                cliente.id = 0;
                cliente.nombre = "";
                cliente.apellido = "";
                cliente.email = "";
                cliente.genero = "";
                cliente.direccion = "";
                cliente.localidad = "";
                cliente.pais = "";
                cliente.fecha_nacimiento = null;

            insertarStackPanel.DataContext = cliente;
            filtrarDataGrid.DataContext = contexto.CLIENTES.Local;
            actualizarDataGrid.DataContext = contexto.CLIENTES.Local;

        }

        private void insertarButton_Click(object sender, RoutedEventArgs e)
        {
            cliente.id = int.Parse(idTextBox.Text);
            cliente.nombre = nombreTextBox.Text;
            cliente.apellido = apellidoTextBox.Text;

            contexto.CLIENTES.Add(cliente);
            contexto.SaveChanges();

            var consulta = from n in contexto.CLIENTES select n;
            clientesListBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            modificarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            eliminarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            contexto.CLIENTES.Remove((CLIENTE)eliminarComboBox.SelectedItem);

            contexto.SaveChanges();

            var consulta = from n in contexto.CLIENTES select n;
            clientesListBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            modificarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            eliminarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
        }

        private void modificarButton_Click(object sender, RoutedEventArgs e)
        {
            CLIENTE CLienteModificar = (CLIENTE)modificarComboBox.SelectedItem;
                CLienteModificar.nombre = nombreModificarTextBox.Text;
                CLienteModificar.apellido = apellidoModificarTextBox.Text;

            contexto.SaveChanges();

            var consulta = from n in contexto.CLIENTES select n;
            clientesListBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            modificarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
            eliminarComboBox.DataContext = new ObservableCollection<CLIENTE>(consulta.ToList());
        }

        private void FiltrarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
