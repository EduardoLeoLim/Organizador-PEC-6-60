using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Persistence;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Application.Export;
using Organizador_PEC_6_60.Instrumento.Application.Search;
using Organizador_PEC_6_60.Instrumento.Application.Update;
using Organizador_PEC_6_60.Instrumento.Infrastructure.Export;
using Organizador_PEC_6_60.Instrumento.Infrastructure.Persistence;
using Organizador_PEC_6_60.Municipio.Application.Search;
using Organizador_PEC_6_60.Municipio.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoEstadistica.Application;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Persistence;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Persistence;

namespace Organizador_PEC_6_60.Instrumento.Infrastructure.Views;

public partial class FindInstrumento : Page
{
    private readonly ManageTipoEstadistica _managerTipoEstadistica;

    public FindInstrumento()
    {
        InitializeComponent();
        _managerTipoEstadistica = new ManageTipoEstadistica(SqliteTipoEstadisticaRepository.Instance);

        LoadForm();
    }

    private void LoadComboBoxInstrumento(object sender, SelectionChangedEventArgs e)
    {
        cbxTipoInstrumento.Items.Clear();
        cbxTipoInstrumento.Items.Add("TODOS");
        if (cbxTipoEstadistica.SelectedItem is TipoEstadisticaData)
        {
            var tipoEstadistica = (TipoEstadisticaData)cbxTipoEstadistica.SelectedItem;
            foreach (var instrumento in tipoEstadistica.Instrumentos)
                cbxTipoInstrumento.Items.Add(instrumento);
        }

        cbxTipoInstrumento.SelectedIndex = 0;
    }

    private void LoadComboBoxMunicipio(object sender, SelectionChangedEventArgs e)
    {
        cbxMunicipio.Items.Clear();
        cbxMunicipio.Items.Add("TODOS");
        if (cbxEntidadFederativa.SelectedItem is DataEntidadFederativa)
        {
            var entidadFederativa = (DataEntidadFederativa)cbxEntidadFederativa.SelectedItem;
            var municipiosByEntidadFederativaSearcher = new SearchMunicipiosByEntidadFederativa(
                new AllMunicipioSeacher(SqliteMunicipioRepository.Instance),
                new EntidadFederativaByIdSearcher(SqliteEntidadFederativaRepository.Instance)
            );
            var municipios = municipiosByEntidadFederativaSearcher
                .SearchByEntidadFederativa(entidadFederativa.Id).Municipios;
            foreach (var municipio in municipios)
                cbxMunicipio.Items.Add(municipio);
        }

        cbxMunicipio.SelectedIndex = 0;
    }

    private void ValidateConsecutivoFormat(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void Find_PEC_6_60_Click(object sender, RoutedEventArgs e)
    {
        var finder = new SearchInstrumentoByCriteria(
            SqliteInstrumentoRepository.Instance,
            SqliteMunicipioRepository.Instance,
            SqliteEntidadFederativaRepository.Instance,
            SqliteTipoEstadisticaRepository.Instance,
            SqliteTipoInstrumentoRepository.Instance
        );

        if (cbxTipoEstadistica.SelectedItem is TipoEstadisticaData)
        {
            var idTipoEstadistica = ((TipoEstadisticaData)cbxTipoEstadistica.SelectedItem).Id;
            finder = finder.TipoEstadistica(idTipoEstadistica);
        }

        if (cbxTipoInstrumento.SelectedItem is TipoInstrumentoResponse)
        {
            var idTipoInstrumento = ((TipoInstrumentoResponse)cbxTipoInstrumento.SelectedItem).Id;
            finder = finder.TipoInstrumento(idTipoInstrumento);
        }

        if (int.TryParse(cbxAñoEstadistico.SelectedItem.ToString(), out _))
        {
            var añoEstadistico = cbxAñoEstadistico.SelectedItem.ToString();
            finder = finder.AñoEstadistico(añoEstadistico);
        }

        if (cbxMesEstadistico.SelectedItem is MesEstadistico)
        {
            var idMesEstadistico = ((MesEstadistico)cbxMesEstadistico.SelectedItem).Id;
            finder = finder.MesEstadistico(idMesEstadistico);
        }

        if (cbxEntidadFederativa.SelectedItem is DataEntidadFederativa)
        {
            var idEntidadFederativa = ((DataEntidadFederativa)cbxEntidadFederativa.SelectedItem).Id;
            finder = finder.EntidadFederativa(idEntidadFederativa);
        }

        if (cbxMunicipio.SelectedItem is DataMunicipio)
        {
            var idMunicipio = ((DataMunicipio)cbxMunicipio.SelectedItem).Id;
            finder = finder.Municipio(idMunicipio);
        }

        int consecutivo;
        if (int.TryParse(txtConsecutivo.Text, out consecutivo)) finder = finder.Consecutivo(consecutivo);

        if (cbxSireso.SelectedValue.ToString() == "SI")
            finder.GuardadoSIRESO(true);

        if (cbxSireso.SelectedValue.ToString() == "NO")
            finder.GuardadoSIRESO(false);

        tblInstrumentos.ItemsSource = finder.SearchInstrumentos().Instrumentos;
        LoadForm();
    }

    private void UpdateStatusSIRESO_isChecked(object sender, RoutedEventArgs e)
    {
        var instrumento = (InstrumentoData)((CheckBox)sender).DataContext;
        var isChecked = ((CheckBox)sender).IsChecked.Value;
        if (isChecked)
        {
            new InstrumentoSavedInSireso(SqliteInstrumentoRepository.Instance).SavedInSIRESO(instrumento.Id);
        }
        else
        {
            var resultado = MessageBox.Show(Window.GetWindow(this),
                $"¿Deseas marcar el instrumento {instrumento.Nombre} como no guardado en SIRESO?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
                new InstrumentoSavedInSireso(SqliteInstrumentoRepository.Instance).UnsavedInSIRESO(instrumento.Id);
            else
                ((CheckBox)e.Source).IsChecked = true;
        }
    }

    private void Show_PEC_6_60Details_Click(object sender, RoutedEventArgs e)
    {
        var idInstrumento = ((InstrumentoData)((Button)sender).DataContext).Id;
        EntidadFederativaByIdSearcherService entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(
            SqliteEntidadFederativaRepository.Instance
        );

        var instrumento = new SearchInstrumentoById(
            SqliteInstrumentoRepository.Instance,
            SqliteMunicipioRepository.Instance,
            entidadFederativaByIdSearcher,
            SqliteTipoEstadisticaRepository.Instance,
            SqliteTipoInstrumentoRepository.Instance
        ).SearchInstrumento(idInstrumento);
        ctrlPEC_6_60Details.LoadDetails(instrumento);
    }

    private void LoadForm()
    {
        AllTipoEstadisticaSearcherService allTipoEstadisticaSearcher = new AllTipoEstadisticaSearcher(
            SqliteTipoEstadisticaRepository.Instance
        );
        var tiposEstadistica = new SearchAllTiposEstadistica(allTipoEstadisticaSearcher)
            .SearchAll().TiposEstadistica;
        cbxTipoEstadistica.Items.Clear();
        cbxTipoEstadistica.Items.Add("TODOS");
        foreach (var tipoEstadistica in tiposEstadistica)
            cbxTipoEstadistica.Items.Add(tipoEstadistica);
        cbxTipoEstadistica.SelectedIndex = 0;

        cbxTipoInstrumento.Items.Clear();
        cbxTipoInstrumento.Items.Add("TODOS");
        cbxTipoInstrumento.SelectedIndex = 0;

        var years = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1);
        cbxAñoEstadistico.Items.Clear();
        cbxAñoEstadistico.Items.Add("TODOS");
        foreach (var year in years)
            cbxAñoEstadistico.Items.Add(year);
        cbxAñoEstadistico.SelectedIndex = 0;

        var months = MesEstadistico.Meses;
        cbxMesEstadistico.Items.Clear();
        cbxMesEstadistico.Items.Add("TODOS");
        foreach (var month in months)
            cbxMesEstadistico.Items.Add(month);
        cbxMesEstadistico.SelectedIndex = 0;

        var allEntidadesFederativasSearcher = new SearchAllEntidadesFederativas(
            new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
        );
        var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
        cbxEntidadFederativa.Items.Clear();
        cbxEntidadFederativa.Items.Add("TODOS");
        foreach (var entidadFederativa in entidadesFederativas)
            cbxEntidadFederativa.Items.Add(entidadFederativa);
        cbxEntidadFederativa.SelectedIndex = 0;

        cbxMunicipio.Items.Clear();
        cbxMunicipio.Items.Add("TODOS");
        cbxMunicipio.SelectedIndex = 0;

        txtConsecutivo.Text = "";

        cbxSireso.Items.Clear();
        cbxSireso.Items.Add("TODOS");
        cbxSireso.Items.Add("SI");
        cbxSireso.Items.Add("NO");
        cbxSireso.SelectedIndex = 0;
    }

    private void CopyPDF_Click(object sender, RoutedEventArgs e)
    {
        var instrumento = (InstrumentoData)((Button)sender).DataContext;
        var wasExported = new ExportInstrumento(new PdfInstrumentoExporter()).Export(instrumento, Path.GetTempPath());

        if (wasExported)
            MessageBox.Show("Ruta del archivo copiada al portapapeles", "Instrumento copiado", MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
        else
            MessageBox.Show("Error al exportar el instrumento, intentalo de nuevo.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
    }
}