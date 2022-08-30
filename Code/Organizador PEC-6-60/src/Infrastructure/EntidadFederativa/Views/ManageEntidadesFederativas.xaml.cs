using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.EntidadFederativa;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Views
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

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            Infrastructure.EntidadFederativa.Views.FormEntidadFederativa form = new Infrastructure.EntidadFederativa.Views.FormEntidadFederativa(_manager);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            EntidadFederativaResponse record = (EntidadFederativaResponse)((Button)e.Source).DataContext;
            Infrastructure.EntidadFederativa.Views.FormEntidadFederativa form = new Infrastructure.EntidadFederativa.Views.FormEntidadFederativa(_manager, record.Id);
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

            MessageBoxResult result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _manager.DeleteEntidadfederativa(record.Id);
                }
                catch (DbException ex)
                {
                    MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                LoadTable();
            }
        }

        private void LoadTable()
        {
            try
            {
                var entidadesFederativas = _manager.SearchAllEntidadesFederativas().EntidadesFederativas;
                tblEntidades.ItemsSource = entidadesFederativas;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                tblEntidades.Items.Clear();
            }
        }
    }
}