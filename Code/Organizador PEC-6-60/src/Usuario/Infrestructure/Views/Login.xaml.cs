using System.Windows;

namespace Organizador_PEC_6_60.Usuario.Infrestructure.Views
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            new RegisterUsuario().Show();
            Close();
        }
    }
}