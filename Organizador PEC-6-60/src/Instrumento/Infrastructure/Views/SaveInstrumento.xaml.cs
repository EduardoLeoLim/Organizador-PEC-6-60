using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Persistence;
using Organizador_PEC_6_60.Instrumento.Application.Create;
using Organizador_PEC_6_60.Instrumento.Infrastructure.Persistence;
using Organizador_PEC_6_60.Municipio.Application.Search;
using Organizador_PEC_6_60.Municipio.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoEstadistica.Application;
using Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoInstrumento.Application;

namespace Organizador_PEC_6_60.Instrumento.Infrastructure.Views;

public partial class SaveInstrumento : Page
{
    private readonly ManageTipoEstadistica _managerTipoEstadistica;

    public SaveInstrumento()
    {
        InitializeComponent();
        _managerTipoEstadistica = new ManageTipoEstadistica(SqliteTipoEstadisticaRepository.Instance);
        LoadForm();
    }

    private void LoadForm(object sender, RoutedEventArgs e)
    {
        LoadForm();
    }

    private void LoadComboBoxInstrumento(object sender, SelectionChangedEventArgs e)
    {
        if (cbxTipoEstadistica.SelectedIndex >= 0)
        {
            var tipoEstadistica = (TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem;
            cbxInstrumento.ItemsSource = tipoEstadistica.Instrumentos;
        }
    }

    private void LoadComboBoxMunicipio(object sender, SelectionChangedEventArgs e)
    {
        if (cbxEntidadFederativa.SelectedIndex >= 0)
        {
            var entidadFederativa = (DataEntidadFederativa)cbxEntidadFederativa.SelectedItem;
            var municipiosByEntidadFederativaSearcher =
                new SearchMunicipiosByEntidadFederativa(
                    new AllMunicipioSeacher(SqliteMunicipioRepository.Instance),
                    new EntidadFederativaByIdSearcher(SqliteEntidadFederativaRepository.Instance)
                );
            cbxMunicipio.ItemsSource = municipiosByEntidadFederativaSearcher
                .SearchByEntidadFederativa(entidadFederativa.Id).Municipios;
        }
    }

    private void SelectFile(object sender, EventArgs args)
    {
        lblFilePath.Text = pdfViewer.DocumentInfo.FilePath + pdfViewer.DocumentInfo.FileName;
    }

    private void ValidateConsecutivoFormat(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void SavePEC_6_60_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            cbxTipoEstadistica.IsEnabled = false;
            cbxInstrumento.IsEnabled = false;
            cbxAñoEstadistico.IsEnabled = false;
            cbxMesEstadistico.IsEnabled = false;
            cbxEntidadFederativa.IsEnabled = false;
            cbxMunicipio.IsEnabled = false;
            txtConsecutivo.IsEnabled = false;
            btnSave.IsEnabled = false;
            pdfViewer.IsEnabled = false;

            if (IsValidFormData())
            {
                var idTipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem).Id;
                var idTipoInstrumento = ((TipoInstrumentoResponse)cbxInstrumento.SelectedItem).Id;
                var idMunicipio = ((DataMunicipio)cbxMunicipio.SelectedItem).Id;
                var añoEstadistico = cbxAñoEstadistico.SelectedValue.ToString();
                var mesEstadistico = (int)((dynamic)cbxMesEstadistico.SelectedItem).Id;
                var consecutivo = int.Parse(txtConsecutivo.Text);

                var memoryStream = new MemoryStream();
                pdfViewer.LoadedDocument.Save(memoryStream);
                var byteArrayInstrumento = memoryStream.ToArray();

                new CreateInstrumento(SqliteInstrumentoRepository.Instance)
                    .CreateNewInstrumento(
                        idTipoEstadistica,
                        idTipoInstrumento,
                        idMunicipio,
                        añoEstadistico,
                        mesEstadistico,
                        consecutivo,
                        byteArrayInstrumento
                    );

                MessageBox.Show(
                    "PEC-6-60 registrado",
                    "Exito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                txtLastSave.Text = pdfViewer.DocumentInfo.FilePath + pdfViewer.DocumentInfo.FileName;
                LoadForm();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            cbxTipoEstadistica.IsEnabled = true;
            cbxInstrumento.IsEnabled = true;
            cbxAñoEstadistico.IsEnabled = true;
            cbxMesEstadistico.IsEnabled = true;
            cbxEntidadFederativa.IsEnabled = true;
            cbxMunicipio.IsEnabled = true;
            txtConsecutivo.IsEnabled = true;
            btnSave.IsEnabled = true;
            pdfViewer.IsEnabled = true;
        }
    }

    private void LoadForm()
    {
        LoadFormStyle();

        cbxTipoEstadistica.ItemsSource = _managerTipoEstadistica.SearchAllTiposEstadisitca().TiposEstadistica;
        cbxInstrumento.ItemsSource = Enumerable.Empty<object>();
        cbxAñoEstadistico.ItemsSource = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1);
        cbxAñoEstadistico.SelectedIndex = -1;
        var months = Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames.Take(12);
        cbxMesEstadistico.ItemsSource = months.Select(item =>
        {
            return new
            {
                Id = months.ToList().IndexOf(item) + 1,
                Nombre = item.ToUpper()
            };
        });
        cbxMesEstadistico.SelectedIndex = -1;

        var allEntidadesFederativasSearcher =
            new SearchAllEntidadesFederativas(
                new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
            );
        cbxEntidadFederativa.ItemsSource = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
        cbxMunicipio.ItemsSource = null;
        txtConsecutivo.Text = "";
        lblFilePath.Text = "";
        pdfViewer.Unload();
    }

    private bool IsValidFormData()
    {
        LoadFormStyle();

        if (IsThereEmptyFields())
        {
            MessageBox.Show("Hay campos vacios en el formulario", "Campos vacios", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return false;
        }

        return true;
    }

    private void LoadFormStyle()
    {
        var textBoxStyle = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
        var comboBoxStyle = System.Windows.Application.Current.TryFindResource(typeof(ComboBox)) as Style;

        cbxTipoEstadistica.Style = comboBoxStyle;
        cbxInstrumento.Style = comboBoxStyle;
        cbxAñoEstadistico.Style = comboBoxStyle;
        cbxMesEstadistico.Style = comboBoxStyle;
        cbxEntidadFederativa.Style = comboBoxStyle;
        cbxMunicipio.Style = comboBoxStyle;
        txtConsecutivo.Style = textBoxStyle;
    }

    private bool IsThereEmptyFields()
    {
        var isThere = false;

        if (!(cbxTipoEstadistica.SelectedIndex >= 0))
        {
            cbxTipoEstadistica.Style =
                System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (!(cbxInstrumento.SelectedIndex >= 0))
        {
            cbxInstrumento.Style = System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (!(cbxAñoEstadistico.SelectedIndex >= 0))
        {
            cbxAñoEstadistico.Style =
                System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (!(cbxMesEstadistico.SelectedIndex >= 0))
        {
            cbxMesEstadistico.Style =
                System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (!(cbxEntidadFederativa.SelectedIndex >= 0))
        {
            cbxEntidadFederativa.Style =
                System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (!(cbxMunicipio.SelectedIndex >= 0))
        {
            cbxMunicipio.Style = System.Windows.Application.Current.FindResource("ComboBox has-error") as Style;
            isThere = true;
        }

        if (txtConsecutivo.Text.Length == 0)
        {
            txtConsecutivo.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
            isThere = true;
        }

        if (pdfViewer.LoadedDocument == null) isThere = true;

        return isThere;
    }
}