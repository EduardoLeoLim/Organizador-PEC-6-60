using System.Windows;
using Organizador_PEC_6_60.EntidadFederativa.Infrestructure.Views;
using Organizador_PEC_6_60.Instrumento.Infrestructure.Views;
using Organizador_PEC_6_60.Municipio.Infrestructure.Views;
using Organizador_PEC_6_60.PEC_6_60.Infrestructure.Views;
using Organizador_PEC_6_60.TipoEstadistica.Infrestructure.Views;
using Organizador_PEC_6_60.Usuario.Application;

namespace Organizador_PEC_6_60.Usuario.Infrestructure.Views
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
            //throw new System.NotImplementedException();
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
    }
}