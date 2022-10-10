using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.TipoEstadistica.Application;
using Organizador_PEC_6_60.TipoEstadistica.Application.Create;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Application.Update;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Exceptions;
using Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoInstrumento.Application;

namespace Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Views;

public partial class FormTipoEstadistica : Window
{
    private readonly bool _isNewRecord;
    private readonly ManageTipoEstadistica _managerTipoEstadistica;
    private readonly ManageTiposInstrumento _managerTiposInstrumento;
    private TipoEstadisticaData _tipoEstadistica;

    public FormTipoEstadistica(
        ManageTipoEstadistica managerTipoEstadistica,
        ManageTiposInstrumento managerTiposInstrumento
    )
    {
        InitializeComponent();
        _managerTipoEstadistica = managerTipoEstadistica;
        _managerTiposInstrumento = managerTiposInstrumento;
        _isNewRecord = true;
        LoadForm();
    }

    public FormTipoEstadistica(
        ManageTipoEstadistica managerTipoEstadistica,
        ManageTiposInstrumento managerTiposInstrumento,
        int idTipoEstadistica
    ) : this(managerTipoEstadistica, managerTiposInstrumento)
    {
        _isNewRecord = false;
        LoadTipoEstadisitca(idTipoEstadistica);
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
                    TipoEstadisticaCreatorService tipoEstadisticaCreator = new TipoEstadisticaCreator(
                        SqliteTipoEstadisticaRepository.Instance
                    );
                    new RegisterTipoEstadistica(tipoEstadisticaCreator).Register(
                        int.Parse(txtClave.Text),
                        txtNombre.Text,
                        GetSelectedInstrumentos()
                    );

                    MessageBox.Show("Tipo de estadística registrada.", "Exito", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    TipoEstadisticaUpdaterService updater = new TipoEstadisticaUpdater(
                        SqliteTipoEstadisticaRepository.Instance
                    );
                    new UpdateTipoEstadistica(updater).Update(
                        _tipoEstadistica.Id,
                        int.Parse(txtClave.Text),
                        txtNombre.Text, GetSelectedInstrumentos()
                    );

                    MessageBox.Show("Tipo de estadística editada.", "Exito", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Close();
                }
            }
        }
        catch (InvalidClaveTipoEstadistica ex)
        {
            txtClave.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidNombreTipoEstadistica ex)
        {
            txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (InvalidInstrumentosTipoEstadistica ex)
        {
            MessageBox.Show(ex.Message, "Error Instrumentos", MessageBoxButton.OK, MessageBoxImage.Error);
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

    private void LoadForm()
    {
        try
        {
            var instrumentos = _managerTiposInstrumento.SearchAllInstrumentos().TiposInstrumento;
            foreach (var instrumento in instrumentos)
            {
                var item = new CheckBox();
                item.DataContext = instrumento;
                item.SetBinding(ContentProperty, "Nombre");
                listInstrumentos.Children.Add(item);
            }

            btnSave.IsEnabled = true;
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            btnSave.IsEnabled = false;
        }
    }

    private void LoadTipoEstadisitca(int idTipoEstadisitica)
    {
        try
        {
            TipoEstadisticaByIdSearcherService byIdSearcher = new TipoEstadisticaByIdSearcher(
                SqliteTipoEstadisticaRepository.Instance
            );
            _tipoEstadistica = new SearchTipoEstadisticaById(byIdSearcher).SearchById(idTipoEstadisitica);
            txtClave.Text = _tipoEstadistica.Clave.ToString();
            txtNombre.Text = _tipoEstadistica.Nombre;

            foreach (var child in listInstrumentos.Children)
                if (child is CheckBox)
                {
                    var checkBox = (CheckBox)child;
                    var instrumento = (TipoInstrumentoResponse)checkBox.DataContext;
                    var count = _tipoEstadistica.Instrumentos.Where(item => item.Id == instrumento.Id).Count();
                    if (count == 1) checkBox.IsChecked = true;
                }
        }
        catch (DbException ex)
        {
            MessageBox.Show(ex.Message, "Error base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            btnSave.IsEnabled = false;
        }
    }

    private List<TipoInstrumentoResponse> GetSelectedInstrumentos()
    {
        var listSelectedInstrumentos = new List<TipoInstrumentoResponse>();

        foreach (var item in listInstrumentos.Children)
            if (item is CheckBox)
            {
                var checkBox = (CheckBox)item;
                if (checkBox.IsChecked == true)
                {
                    var instrumento = (TipoInstrumentoResponse)checkBox.DataContext;
                    listSelectedInstrumentos.Add(instrumento);
                }
            }

        return listSelectedInstrumentos;
    }

    private bool IsValidFormData()
    {
        txtClave.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        txtNombre.Style = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;

        if (IsThereEmptyFields())
        {
            MessageBox.Show("Hay campos vacios en el formulario.", "Campos vacíos", MessageBoxButton.OK,
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

        return isThere;
    }
}