using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Domain.Exceptions;

namespace Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Views;

public partial class FormTipoInstrumento : Window
{
    private readonly bool _isNewRecord;
    private readonly Application.ManageTiposInstrumento _managerTiposInstrumentos;
    private TipoInstrumentoResponse _instrumento;

    public FormTipoInstrumento(Application.ManageTiposInstrumento managerTiposInstrumentos)
    {
        InitializeComponent();
        _managerTiposInstrumentos = managerTiposInstrumentos;
        _isNewRecord = true;
    }

    public FormTipoInstrumento(Application.ManageTiposInstrumento managerTiposInstrumentos, int idInstrumento) : this(
        managerTiposInstrumentos)
    {
        _isNewRecord = false;
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
                if (_isNewRecord)
                {
                    _managerTiposInstrumentos.RegisterInstrumento(txtNombre.Text);

                    MessageBox.Show("Tipo de Instrumento registrado.", "Exito", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    _managerTiposInstrumentos.UpdateInstrumento(_instrumento.Id, txtNombre.Text);

                    MessageBox.Show("Tipo de Instrumento editado.", "Exito", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Close();
                }
            }
        }
        catch (InvalidNombreTipoInstrumento ex)
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
            _instrumento = _managerTiposInstrumentos.SearchInstrumentoById(idInstrumento);
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
        var isThere = false;

        if (txtNombre.Text.Length == 0)
        {
            txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        return isThere;
    }
}