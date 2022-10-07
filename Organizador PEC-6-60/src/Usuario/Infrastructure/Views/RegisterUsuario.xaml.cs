using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Usuario.Application.Create;
using Organizador_PEC_6_60.Usuario.Domain.Exceptions;
using Organizador_PEC_6_60.Usuario.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.Usuario.Infrastructure.Views;

public partial class RegisterUsuario : Window
{
    private readonly CreateUsuario _createUsuario;

    public RegisterUsuario()
    {
        InitializeComponent();
        _createUsuario = new CreateUsuario(SqliteUsuarioRepository.Instance);
    }

    private void RegisterUsuario_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (IsValidFormData())
            {
                _createUsuario.RegisterUsuario(
                    txtUsername.Text,
                    txtPassword.Password,
                    txtPassword2.Password,
                    txtNombre.Text,
                    txtApellidos.Text
                );

                MessageBox.Show(
                    $"Usuario: {txtUsername.Text}\nContraseña: {txtPassword.Password}",
                    "Usuario registrado",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                new Login().Show();
                Close();
            }
        }
        catch (InvalidUsername ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error Nombre",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidPassword ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error Contraseña",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidNombre ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error Nombre",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidApellidos ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error Apellidos",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (DbException ex)
        {
            MessageBox.Show(
                ex.ToString(),
                "Error de base de datos",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        new Login().Show();
        Close();
    }

    private bool IsValidFormData()
    {
        txtNombre.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtApellidos.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtUsername.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtPassword.Style = System.Windows.Application.Current.TryFindResource(typeof(PasswordBox)) as Style;
        txtPassword2.Style = System.Windows.Application.Current.TryFindResource(typeof(PasswordBox)) as Style;

        if (IsThereEmptyFields())
        {
            MessageBox.Show(
                "Hay campos vacos en el formulario",
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
        var result = false;

        if (txtNombre.Text.Length == 0)
        {
            txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            result = true;
        }

        if (txtApellidos.Text.Length == 0)
        {
            txtApellidos.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            result = true;
        }

        if (txtUsername.Text.Length == 0)
        {
            txtUsername.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            result = true;
        }

        if (txtPassword.Password.Length == 0)
        {
            txtPassword.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            result = true;
        }

        if (txtPassword2.Password.Length == 0)
        {
            txtPassword2.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            result = true;
        }

        return result;
    }
}