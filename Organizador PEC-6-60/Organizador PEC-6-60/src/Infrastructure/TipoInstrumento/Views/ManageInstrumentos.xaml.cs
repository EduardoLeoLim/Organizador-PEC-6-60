using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.TipoInstrumento;
using Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Views
{
    public partial class ManageInstrumentos : Page
    {
        private readonly ManageTiposInstrumento _managerTiposInstrumentos;

        public ManageInstrumentos()
        {
            InitializeComponent();
            _managerTiposInstrumentos = new ManageTiposInstrumento(new SqliteTipoInstrumentoRepository());
            LoadTable();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormInstrumento form = new FormInstrumento(_managerTiposInstrumentos);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            TipoInstrumentoResponse record = (TipoInstrumentoResponse)((Button)e.Source).DataContext;
            FormInstrumento form = new FormInstrumento(_managerTiposInstrumentos, record.Id);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            TipoInstrumentoResponse record = (TipoInstrumentoResponse)((Button)e.Source).DataContext;
            string message = "¿Quiere eliminar el registro?";
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
                    _managerTiposInstrumentos.DeleteInstrumento(record.Id);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
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
                var instrumentos = _managerTiposInstrumentos.SearchAllInstrumentos().TiposInstrumento;
                tblInstrumentos.ItemsSource = instrumentos;
            }
            catch (DbException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error base de datos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                tblInstrumentos.Items.Clear();
            }
        }
    }
}