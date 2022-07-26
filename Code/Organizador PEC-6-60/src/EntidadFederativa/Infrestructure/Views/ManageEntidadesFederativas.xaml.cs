using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.EntidadFederativa.Application;
using Organizador_PEC_6_60.EntidadFederativa.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.EntidadFederativa.Infrestructure.Views
{
    public partial class ManageEntidadesFederativas : Page
    {
        private readonly ManageEntidadFederativa _manager;
        public ManageEntidadesFederativas()
        {
            InitializeComponent();
            _manager = new ManageEntidadFederativa(new SqliteEntidadFederativaRepository());
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormEntidadFederativa form = new FormEntidadFederativa(_manager);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            EntidadFederativaResponse record = (EntidadFederativaResponse)((Button)e.Source).DataContext;
            FormEntidadFederativa form = new FormEntidadFederativa(_manager, record.Id);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            EntidadFederativaResponse record = (EntidadFederativaResponse)((Button)e.Source).DataContext;
            string message = "¿Quiere eliminar el registro?";
            message += $"\nClave: {record.Clave}";
            message += $"\nNombre: {record.Nombre}";

            MessageBoxResult result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                _manager.DeleteEntidadfederativa(record.Id);
                
                LoadTable();
            }
        }

        private void LoadTable()
        {
            var entidadesFederativas = _manager.SearchAllEntidadesFederativas().EntidadesFederativas;
            tblEntidades.ItemsSource = entidadesFederativas;
        }
    }
}

