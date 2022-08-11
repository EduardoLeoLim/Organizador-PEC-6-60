using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Domain.Exceptions;

namespace Organizador_PEC_6_60.Instrumento.Infrestructure.Views
{
    public partial class FormInstrumento : Window
    {
        private readonly ManageInstrumento _managerInstrumentos;
        private bool isNewRecord;
        private InstrumentoResponse _instrumento;

        public FormInstrumento(ManageInstrumento managerInstrumentos)
        {
            InitializeComponent();
            _managerInstrumentos = managerInstrumentos;
            isNewRecord = true;
        }

        public FormInstrumento(ManageInstrumento managerInstrumentos, int idInstrumento) : this(managerInstrumentos)
        {
            isNewRecord = false;
            LoadForm(idInstrumento);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtNombre.IsEnabled = false;
                btnSave.IsEnabled = false;

                if (IsValidFormData())
                {
                    if (isNewRecord)
                    {
                        _managerInstrumentos.RegisterInstrumento(txtNombre.Text);
                        MessageBox.Show("Instrumento registrado.", "Exito", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        _managerInstrumentos.UpdateInstrumento(_instrumento.Id, txtNombre.Text);
                        MessageBox.Show("Instrumento editado.", "Exito", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                }
            }
            catch (InvalidNombreInstrumento ex)
            {
                txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
                MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                txtNombre.IsEnabled = true;
                btnSave.IsEnabled = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadForm(int idInstrumento)
        {
            try
            {
                _instrumento = _managerInstrumentos.SearchInstrumentoById(idInstrumento);
                txtNombre.Text = _instrumento.Nombre;
            }
            catch (DbException ex)
            {
                btnSave.IsEnabled = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidFormData()
        {
            txtNombre.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;

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

            if (txtNombre.Text.Length == 0)
            {
                txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
                isThere = true;
            }

            return isThere;
        }
    }
}