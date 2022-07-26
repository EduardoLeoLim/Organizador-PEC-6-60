﻿using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.TipoEstadistica.Application.Delete;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Views;

public partial class ManageTiposEstadistica : Page
{
    public ManageTiposEstadistica()
    {
        InitializeComponent();
        LoadTable();
    }

    private void LoadWindow(object sender, RoutedEventArgs e)
    {
        LoadTable();
    }

    private void NewRecord_Click(object sender, RoutedEventArgs e)
    {
        var form = new FormTipoEstadistica(
            new ManageTiposInstrumento(SqliteTipoInstrumentoRepository.Instance)
        );
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void EditRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (TipoEstadisticaData)((Button)e.Source).DataContext;
        var form = new FormTipoEstadistica(
            new ManageTiposInstrumento(SqliteTipoInstrumentoRepository.Instance),
            record.Id
        );
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void DeleteRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (TipoEstadisticaData)((Button)e.Source).DataContext;
        var message = "¿Quiere eliminar el registro?";
        message += $"\nClave: {record.Clave}";
        message += $"\nNombre: {record.Nombre}";

        var result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            TipoEstadisticaDeleterService tipoEstadisticaDeleter = new TipoEstadisticaDeleter(
                SqliteTipoEstadisticaRepository.Instance
            );
            new DeleteTipoEstadistica(tipoEstadisticaDeleter).Delete(record.Id);
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        LoadTable();
    }

    private void LoadTable()
    {
        try
        {
            AllTipoEstadisticaSearcherService allTipoEstadisticaSearcher = new AllTipoEstadisticaSearcher(
                SqliteTipoEstadisticaRepository.Instance
            );
            var tiposEstadistica = new SearchAllTiposEstadistica(allTipoEstadisticaSearcher)
                .SearchAll().TiposEstadistica;
            tblTiposEstadistica.ItemsSource = tiposEstadistica;
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);

            tblTiposEstadistica.Items.Clear();
        }
    }
}