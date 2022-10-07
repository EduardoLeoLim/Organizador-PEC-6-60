using System;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Persistence;
using Organizador_PEC_6_60.Municipio.Application.Create;
using Organizador_PEC_6_60.Municipio.Application.Search;
using Organizador_PEC_6_60.Municipio.Application.Update;
using Organizador_PEC_6_60.Municipio.Domain.Exceptions;
using Organizador_PEC_6_60.Municipio.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.Municipio.Infrastructure.Views;

public partial class FormMunicipio : Window
{
    private readonly bool _isNewRecord;
    private DataMunicipio _municipio;

    public FormMunicipio()
    {
        InitializeComponent();
        _isNewRecord = true;
        var allEntidadesFederativasSearcher =
            new SearchAllEntidadesFederativas(
                new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
            );
        var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
        cbxEntidadFederativa.ItemsSource = entidadesFederativas;
    }

    public FormMunicipio(int idMunicipio) : this()
    {
        _isNewRecord = false;
        LoadForm(idMunicipio);
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            cbxEntidadFederativa.IsEnabled = false;
            txtClave.IsEnabled = false;
            txtNombre.IsEnabled = false;

            if (IsValidFormData())
            {
                var dataEntidadFederativaSeleccionada =
                    (DataEntidadFederativa)cbxEntidadFederativa.SelectionBoxItem;

                if (_isNewRecord)
                {
                    var municipioCreator =
                        new RegisterMunicipio(new MunicipioCreator(SqliteMunicipioRepository.Instance));

                    municipioCreator.Register(
                        int.Parse(txtClave.Text),
                        txtNombre.Text,
                        dataEntidadFederativaSeleccionada
                    );

                    MessageBox.Show(
                        "Municipio registrado.",
                        "Exito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    Close();
                }
                else
                {
                    var municipioUpdater =
                        new UpdateMunicipio(new MunicipioUpdater(SqliteMunicipioRepository.Instance));

                    municipioUpdater.Update(
                        _municipio.Id,
                        int.Parse(txtClave.Text),
                        txtNombre.Text,
                        dataEntidadFederativaSeleccionada
                    );

                    MessageBox.Show(
                        "Municipio actualizado.",
                        "Exito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );

                    Close();
                }
            }
        }
        catch (InvalidClaveMunicipio ex)
        {
            txtClave.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            MessageBox.Show(
                ex.Message,
                "Error Clave",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }
        catch (InvalidNombreMunicipio ex)
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
                MessageBoxImage.Error);
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
            cbxEntidadFederativa.IsEnabled = true;
            txtClave.IsEnabled = true;
            txtNombre.IsEnabled = true;
        }
    }

    private void ValidateClaveFormat(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void LoadForm(int idMunicipio)
    {
        try
        {
            var municipioByIdSearcher = new SearchMunicipioById(
                new MunicipioByIdSearcher(SqliteMunicipioRepository.Instance),
                new EntidadFederativaByIdSearcher(SqliteEntidadFederativaRepository.Instance)
            );
            _municipio = municipioByIdSearcher.SearchById(idMunicipio);
            txtClave.Text = _municipio.Clave.ToString();
            txtNombre.Text = _municipio.Nombre;
            var entidades = cbxEntidadFederativa.ItemsSource.Cast<DataEntidadFederativa>();
            var indexEntidadFederativa =
                entidades.ToList().FindIndex(item => item.Id == _municipio.DataEntidadFederativa.Id);

            cbxEntidadFederativa.SelectedIndex = indexEntidadFederativa;
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
        cbxEntidadFederativa.Style = System.Windows.Application.Current.TryFindResource(typeof(ComboBox)) as Style;

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

        if (cbxEntidadFederativa.SelectedIndex < 0)
        {
            cbxEntidadFederativa.Style =
                System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        return isThere;
    }
}