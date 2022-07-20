using Syncfusion.Licensing;
using System.Configuration;
using System.Windows;

using Organizador_PEC_6_60.Views;

namespace Organizador_PEC_6_60
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var syncfusionLicense = ConfigurationManager.AppSettings["SyncfusionLicense"];
            SyncfusionLicenseProvider.RegisterLicense(syncfusionLicense);
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            new Login().Show();
        }
    }
}