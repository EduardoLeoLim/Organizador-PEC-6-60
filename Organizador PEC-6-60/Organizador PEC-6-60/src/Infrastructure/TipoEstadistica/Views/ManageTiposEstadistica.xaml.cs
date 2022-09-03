using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.TipoEstadistica;
using Organizador_PEC_6_60.Application.TipoInstrumento;
using Organizador_PEC_6_60.Infrastructure.TipoEstadistica.Persistence;
using Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.TipoEstadistica.Views
{
    public partial class ManageTiposEstadistica : Page
    {
        private readonly ManageTipoEstadistica _managerTipoEstadistica;

        public ManageTiposEstadistica()
        {
            InitializeComponent();
            _managerTipoEstadistica = new ManageTipoEstadistica(SqliteTipoEstadisticaRepository.Instance);
            LoadTable();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormTipoEstadistica form = new FormTipoEstadistica(
                _managerTipoEstadistica,
                new ManageTiposInstrumento(SqliteTipoInstrumentoRepository.Instance)
            );
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            TipoEstadisticaResponse record = (TipoEstadisticaResponse)((Button)e.Source).DataContext;
            FormTipoEstadistica form = new FormTipoEstadistica(
                _managerTipoEstadistica,
                new ManageTiposInstrumento(SqliteTipoInstrumentoRepository.Instance),
                record.Id
            );
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            TipoEstadisticaResponse record = (TipoEstadisticaResponse)((Button)e.Source).DataContext;
            string message = "¿Quiere eliminar el registro?";
            message += $"\nClave: {record.Clave}";
            message += $"\nNombre: {record.Nombre}";

            MessageBoxResult result = MessageBox.Show(
                message,
                "Eliminar",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _managerTipoEstadistica.DeleteTipoEstadisitca(record.Id);
                }
                catch (DbException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error base de datos",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }

                LoadTable();
            }
        }

        private void LoadTable()
        {
            try
            {
                var tiposEstadistica = _managerTipoEstadistica.SearchAllTiposEstadisitca().TiposEstadistica;
                tblTiposEstadistica.ItemsSource = tiposEstadistica;
            }
            catch (DbException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error base de datos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                tblTiposEstadistica.Items.Clear();
            }
        }
    }
}