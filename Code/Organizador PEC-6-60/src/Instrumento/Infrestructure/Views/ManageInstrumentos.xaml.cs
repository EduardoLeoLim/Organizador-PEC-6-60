using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.Instrumento.Infrestructure.Views
{
    public partial class ManageInstrumentos : Page
    {
        private readonly ManageInstrumento _managerInstrumentos;

        public ManageInstrumentos()
        {
            InitializeComponent();
            _managerInstrumentos = new ManageInstrumento(new SqliteInstrumentoRepository());
            LoadTable();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormInstrumento form = new FormInstrumento(_managerInstrumentos);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            InstrumentoResponse record = (InstrumentoResponse)((Button)e.Source).DataContext;
            FormInstrumento form = new FormInstrumento(_managerInstrumentos, record.Id);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            InstrumentoResponse record = (InstrumentoResponse)((Button)e.Source).DataContext;
            string message = "¿Quiere eliminar el registro?";
            message += $"\nNombre: {record.Nombre}";

            MessageBoxResult result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _managerInstrumentos.DeleteInstrumento(record.Id);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                var instrumentos = _managerInstrumentos.SearchAllInstrumentos().Instrumentos;
                tblInstrumentos.ItemsSource = instrumentos;
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                tblInstrumentos.Items.Clear();
            }
        }
    }
}