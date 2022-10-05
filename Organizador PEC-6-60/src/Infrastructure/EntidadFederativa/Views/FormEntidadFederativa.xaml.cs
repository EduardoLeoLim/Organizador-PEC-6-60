using System;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Application.EntidadFederativa.Create;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.EntidadFederativa.Update;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Exceptions;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Views;

public partial class FormEntidadFederativa : Window
{
    private DataEntidadFederativa _dataEntidadFederativa;
    private readonly bool _isNewRecord;

    public FormEntidadFederativa()
    {
        InitializeComponent();
        _isNewRecord = true;
    }

    public FormEntidadFederativa(int idEntidadFederativa) : this()
    {
        _isNewRecord = false;
        LoadForm(idEntidadFederativa);
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            txtClave.IsEnabled = false;
            txtNombre.IsEnabled = false;
            btnSave.IsEnabled = false;

            if (IsValidFormData())
            {
                if (_isNewRecord)
                {
                    var creator =
                        new RegisterEntidadFederativa(
                            new EntidadFederativaCreator(SqliteEntidadFederativaRepository.Instance)
                        );
                    creator.Register(int.Parse(txtClave.Text), txtNombre.Text);

                    MessageBox.Show(
                        "Entidad Federativa registrada.",
                        "Exito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    Close();
                }
                else
                {
                    var entidadFederativaUpdater =
                        new UpdateEntidadFederativa(
                            new EntidadFederativaUpdater(SqliteEntidadFederativaRepository.Instance)
                        );
                    entidadFederativaUpdater.Update(
                        _dataEntidadFederativa.Id,
                        int.Parse(txtClave.Text),
                        txtNombre.Text
                    );

                    MessageBox.Show(
                        "Entidad Federativa editada.",
                        "Exito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    Close();
                }
            }
        }
        catch (InvalidClaveEntidadFederativa ex)
        {
            txtClave.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            MessageBox.Show(
                ex.Message,
                "Error Clave",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidNombreEntidadFederativa ex)
        {
            txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            MessageBox.Show(
                ex.Message,
                "Error Nombre",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (DbException ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error base de datos",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        finally
        {
            txtClave.IsEnabled = true;
            txtNombre.IsEnabled = true;
            btnSave.IsEnabled = true;
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ValidateClaveFormat(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void LoadForm(int idEntidadFederativa)
    {
        try
        {
            var entidadFederativaByIdSearcher =
                new SearchEntidadFederativaById(
                    new EntidadFederativaByIdSearcher(SqliteEntidadFederativaRepository.Instance)
                );
            _dataEntidadFederativa = entidadFederativaByIdSearcher.SearchById(idEntidadFederativa);
            txtClave.Text = _dataEntidadFederativa.Clave.ToString();
            txtNombre.Text = _dataEntidadFederativa.Nombre;
        }
        catch (DbException ex)
        {
            btnSave.IsEnabled = false;
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool IsValidFormData()
    {
        txtClave.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtNombre.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;

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

        if (txtClave.Text.Length == 0)
        {
            txtClave.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        if (txtNombre.Text.Length == 0)
        {
            txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        return isThere;
    }
}