using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.EntidadFederativa.Application.Delete;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Views;

public partial class ManageEntidadesFederativas : Page
{
    public ManageEntidadesFederativas()
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
        var form = new FormEntidadFederativa();
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void EditRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (DataEntidadFederativa)((Button)e.Source).DataContext;
        var form = new FormEntidadFederativa(record.Id);
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadTable();
    }

    private void DeleteRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (DataEntidadFederativa)((Button)e.Source).DataContext;
        var message = "¿Quiere eliminar el registro?";
        message += $"\nClave: {record.Clave}";
        message += $"\nNombre: {record.Nombre}";

        var result = MessageBox.Show(message, "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            var entidadFederativaDeleter = new DeleteEntidadFederativa(
                new EntidadFederativaDeleter(SqliteEntidadFederativaRepository.Instance)
            );

            entidadFederativaDeleter.Delete(record.Id);
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
            var allEntidadesFederativasSearcher = new SearchAllEntidadesFederativas(
                new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
            );
            var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
            tblEntidades.ItemsSource = entidadesFederativas;
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            tblEntidades.Items.Clear();
        }
    }
}