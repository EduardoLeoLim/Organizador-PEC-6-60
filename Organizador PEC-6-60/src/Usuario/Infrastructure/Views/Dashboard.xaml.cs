using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Views;
using Organizador_PEC_6_60.Instrumento.Infrastructure.Views;
using Organizador_PEC_6_60.Municipio.Infrastructure.Views;
using Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Views;
using Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Views;
using Organizador_PEC_6_60.Usuario.Application;

namespace Organizador_PEC_6_60.Usuario.Infrastructure.Views;

public partial class Dashboard : Window
{
    private readonly FindInstrumento _windowFindPec660;
    private readonly ManageEntidadesFederativas _windowManageEntidadFederativa;
    private readonly ManageInstrumentos _windowManageInstrumento;
    private readonly ManageMunicipios _windowManageMunicipio;
    private readonly ManageTiposEstadistica _windowManageTipoEstadistica;
    private readonly SaveInstrumento _windowSavePec660;
    private UsuarioConnected _usuarioConnected;

    public Dashboard(UsuarioConnected usuarioConnected)
    {
        InitializeComponent();
        _usuarioConnected = usuarioConnected;

        _windowSavePec660 = new SaveInstrumento();
        _windowFindPec660 = new FindInstrumento();
        _windowManageTipoEstadistica = new ManageTiposEstadistica();
        _windowManageInstrumento = new ManageInstrumentos();
        _windowManageEntidadFederativa = new ManageEntidadesFederativas();
        _windowManageMunicipio = new ManageMunicipios();
    }

    private void Dashboard_Click(object sender, RoutedEventArgs e)
    {
        foreach (var item in menu.Children)
            if (item is Button)
            {
                var lblContentButton = (Label)btnSavePEC_6_60.Template.FindName("ContentButton", (Button)item);
                if (lblContentButton.Visibility == Visibility.Visible)
                    lblContentButton.Visibility = Visibility.Collapsed;
                else
                    lblContentButton.Visibility = Visibility.Visible;
            }
    }

    private void SavePEC_6_60_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowSavePec660;
    }

    private void FindPEC_6_60_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowFindPec660;
    }

    private void ManageTiposEstadistica_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowManageTipoEstadistica;
    }

    private void ManageInstrumentos_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowManageInstrumento;
    }

    private void ManageEntidadesFederativas_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowManageEntidadFederativa;
    }

    private void ManageMunicipios_Click(object sender, RoutedEventArgs e)
    {
        mainPanel.Content = _windowManageMunicipio;
    }

    private void LogOut_Click(object sender, RoutedEventArgs e)
    {
        new Login().Show();
        Close();
    }
}