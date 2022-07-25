using System;
using System.Windows;
using Organizador_PEC_6_60.Usuario.Application;
using Organizador_PEC_6_60.Usuario.Application.LogIn;
using Organizador_PEC_6_60.Usuario.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.Usuario.Infrestructure.Views
{
    public partial class Login : Window
    {
        public readonly LogInUsuario _LogInUsuario;

        public Login()
        {
            InitializeComponent();
            _LogInUsuario = new LogInUsuario(new SqliteUsuarioRepository());
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtUsername.IsEnabled = false;
                txtPassword.IsEnabled = false;
                btnLogIn.IsEnabled = false;
                btnSignIn.IsEnabled = false;

                if (IsValidDataForm())
                {
                    UsuarioConnected usuarioLogged = _LogInUsuario.LogIn(txtUsername.Text, txtPassword.Password);
                    new Dashboard(usuarioLogged).Show();
                    Close();
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Credenciales inválidas", "Error inicio sesión", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                txtUsername.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnLogIn.IsEnabled = true;
                btnSignIn.IsEnabled = true;
            }
        }

        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            new RegisterUsuario().Show();
            Close();
        }

        private bool IsValidDataForm()
        {
            if (IsThereEmptyFields())
            {
                MessageBox.Show("Hay campos vacios en el formulario", "Campos vacios", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool IsThereEmptyFields()
        {
            bool isThere = false;

            if (txtUsername.Text.Length == 0)
            {
                txtUsername.Style = new Style();
                isThere = true;
            }

            if (txtPassword.Password.Length == 0)
            {
                txtPassword.Style = new Style();
                isThere = true;
            }

            return isThere;
        }
    }
}