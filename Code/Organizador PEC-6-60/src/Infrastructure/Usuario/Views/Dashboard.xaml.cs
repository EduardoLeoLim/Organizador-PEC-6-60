using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.Usuario;
using Organizador_PEC_6_60.PEC_6_60.Infrestructure.Views;
using ManageEntidadesFederativas = Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Views.ManageEntidadesFederativas;
using ManageInstrumentos = Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Views.ManageInstrumentos;
using ManageMunicipios = Organizador_PEC_6_60.Infrastructure.Municipio.Views.ManageMunicipios;
using ManageTiposEstadistica = Organizador_PEC_6_60.Infrastructure.TipoEstadistica.Views.ManageTiposEstadistica;

namespace Organizador_PEC_6_60.Infrastructure.Usuario.Views
{
    public partial class Dashboard : Window
    {
        private UsuarioConnected _usuarioConnected;
        private readonly SavePEC_6_60 _windowSavePec660;
        private readonly FindPEC_6_60 _windowFindPec660;
        private readonly ManageTiposEstadistica _windowManageTipoEstadistica;
        private readonly ManageInstrumentos _windowManageInstrumento;
        private readonly ManageEntidadesFederativas _windowManageEntidadFederativa;
        private readonly ManageMunicipios _windowManageMunicipio;

        public Dashboard(UsuarioConnected usuarioConnected)
        {
            InitializeComponent();
            _usuarioConnected = usuarioConnected;

            _windowSavePec660 = new SavePEC_6_60();
            _windowFindPec660 = new FindPEC_6_60();
            _windowManageTipoEstadistica = new ManageTiposEstadistica();
            _windowManageInstrumento = new ManageInstrumentos();
            _windowManageEntidadFederativa = new ManageEntidadesFederativas();
            _windowManageMunicipio = new ManageMunicipios();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in menu.Children)
            {
                if (item is Button)
                {
                    Label lblContentButton = (Label)btnSavePEC_6_60.Template.FindName("ContentButton", (Button)item);
                    if (lblContentButton.Visibility == Visibility.Visible)
                        lblContentButton.Visibility = Visibility.Collapsed;
                    else
                        lblContentButton.Visibility = Visibility.Visible;
                }       
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
}