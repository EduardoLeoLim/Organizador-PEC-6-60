using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio.Delete;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;
using Organizador_PEC_6_60.Infrastructure.Municipio.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.Municipio.Views;

public partial class ManageMunicipios : Page
{
    public ManageMunicipios()
    {
        InitializeComponent();
    }

    private void LoadWindow(object sender, RoutedEventArgs e)
    {
        LoadPage();
    }

    private void FindMunicipios_Click(object sender, RoutedEventArgs e)
    {
        if (cbxEntidadFederativa.SelectedIndex >= 0)
        {
            var entidadFederativaSelecionada =
                (DataEntidadFederativa)cbxEntidadFederativa.SelectionBoxItem;
            var municipiosByEntidadFederativaSearcher =
                new SearchMunicipiosByEntidadFederativa(
                    new AllMunicipioSeacher(SqliteMunicipioRepository.Instance),
                    new EntidadFederativaByIdSearcher(SqliteEntidadFederativaRepository.Instance)
                );
            var municipios = municipiosByEntidadFederativaSearcher
                .SearchByEntidadFederativa(entidadFederativaSelecionada.Id).Municipios;
            tblMunicipios.ItemsSource = municipios;
        }
    }

    private void NewRecord_Click(object sender, RoutedEventArgs e)
    {
        var form = new FormMunicipio();
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadPage();
    }

    private void EditRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (DataMunicipio)((Button)e.Source).DataContext;
        var form = new FormMunicipio(record.Id);
        form.Owner = Window.GetWindow(this);
        form.ShowDialog();
        LoadPage();
    }

    private void DeleteRecord_Click(object sender, RoutedEventArgs e)
    {
        var record = (DataMunicipio)((Button)e.Source).DataContext;
        var message = "¿Quiere eliminar el registro?";
        message += $"\nClave: {record.Clave}";
        message += $"\nNombre: {record.Nombre}";

        var result = MessageBox.Show(
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
                var municipioDeleter =
                    new DeleteMunicipio(new MunicipioDeleter(SqliteMunicipioRepository.Instance));
                municipioDeleter.Delete(record.Id);
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

            LoadPage();
        }
    }

    private void LoadPage()
    {
        var allEntidadesFederativasSearcher =
            new SearchAllEntidadesFederativas(
                new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
            );
        var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
        cbxEntidadFederativa.ItemsSource = entidadesFederativas;
        tblMunicipios.ItemsSource = null;
    }
}