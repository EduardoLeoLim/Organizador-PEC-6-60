using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Views;

public partial class ManageTiposInstrumentos : Page
{
    private readonly ManageTiposInstrumento _managerTiposInstrumentos;

    public ManageTiposInstrumentos()
    {
        InitializeComponent();
        _managerTiposInstrumentos = new Application.ManageTiposInstrumento(SqliteTipoInstrumentoRepository.Instance);
        LoadTable();
    }

    private void LoadWindow(object sender, RoutedEventArgs e)
    {
        LoadTable();
    }

    private void NewRecord_Click(object sender, RoutedEventArgs e)
    {
        var form = new FormTipoInstrumento(_managerTiposInstrumentos);
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void EditRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (TipoInstrumentoResponse)((Button)e.Source).DataContext;
        var form = new FormTipoInstrumento(_managerTiposInstrumentos, record.Id);
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void DeleteRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (TipoInstrumentoResponse)((Button)e.Source).DataContext;
        var message = "¿Quiere eliminar el registro?";
        message += $"\nNombre: {record.Nombre}";

        var result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                _managerTiposInstrumentos.DeleteInstrumento(record.Id);
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
            var instrumentos = _managerTiposInstrumentos.SearchAllInstrumentos().TiposInstrumento;
            tblInstrumentos.ItemsSource = instrumentos;
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            tblInstrumentos.Items.Clear();
        }
    }
}