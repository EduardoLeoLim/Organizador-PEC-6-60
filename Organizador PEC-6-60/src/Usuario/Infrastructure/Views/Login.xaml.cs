using System;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Usuario.Application.LogIn;
using Organizador_PEC_6_60.Usuario.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.Usuario.Infrastructure.Views;

public partial class Login : Window
{
    private readonly LogInUsuario _logInUsuario;

    public Login()
    {
        InitializeComponent();
        _logInUsuario = new LogInUsuario(SqliteUsuarioRepository.Instance);
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
                var usuarioLogged = _logInUsuario.LogIn(txtUsername.Text, txtPassword.Password);
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
        txtUsername.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtPassword.Style = System.Windows.Application.Current.TryFindResource(typeof(PasswordBox)) as Style;

        if (IsThereEmptyFields())
        {
            MessageBox.Show(
                "Hay campos vacios en el formulario",
                "Campos vacios",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );

            return false;
        }

        return true;
    }

    private bool IsThereEmptyFields()
    {
        var isThere = false;

        if (txtUsername.Text.Length == 0)
        {
            txtUsername.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        if (txtPassword.Password.Length == 0)
        {
            txtPassword.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        return isThere;
    }
}