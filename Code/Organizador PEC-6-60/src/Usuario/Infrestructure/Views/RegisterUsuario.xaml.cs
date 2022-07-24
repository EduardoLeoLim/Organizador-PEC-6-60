using System.Data.Common;
using System.Windows;
using Organizador_PEC_6_60.Usuario.Application.Create;
using Organizador_PEC_6_60.Usuario.Domain.Exceptions;
using Organizador_PEC_6_60.Usuario.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.Usuario.Infrestructure.Views
{
    public partial class RegisterUsuario : Window
    {
        private readonly CreateUsuario _createUsuario;

        public RegisterUsuario()
        {
            InitializeComponent();
            UsuarioCreator usuarioCreator = new UsuarioCreator(new SqliteUsuarioRepository());
            _createUsuario = new CreateUsuario(usuarioCreator);
        }

        private void RegisterUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsEmptyFormData())
                {
                    _createUsuario.RegisterUsuario(txtUsername.Text, txtPassword.Password, txtNombre.Text,
                        txtApellidos.Text);
                }
            }
            catch (InvalidUsername ex)
            {
                MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidPassword ex)
            {
                MessageBox.Show(ex.Message, "Error Contraseña", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidNombre ex)
            {
                MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidApellidos ex)
            {
                MessageBox.Show(ex.Message, "Error Apellidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message, "Eroro de base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }

        private bool IsEmptyFormData()
        {
            return true; //There are empty fields in the forn
        }
    }
}