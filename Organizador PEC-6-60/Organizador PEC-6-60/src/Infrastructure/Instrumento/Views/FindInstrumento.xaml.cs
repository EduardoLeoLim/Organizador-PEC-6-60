using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Application.EntidadFederativa;
using Organizador_PEC_6_60.Application.Instrumento;
using Organizador_PEC_6_60.Application.Instrumento.Search;
using Organizador_PEC_6_60.Application.Instrumento.Update;
using Organizador_PEC_6_60.Application.Municipio;
using Organizador_PEC_6_60.Application.TipoEstadistica;
using Organizador_PEC_6_60.Application.TipoInstrumento;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;
using Organizador_PEC_6_60.Infrastructure.Instrumento.Persistence;
using Organizador_PEC_6_60.Infrastructure.Municipio.Persistence;
using Organizador_PEC_6_60.Infrastructure.TipoEstadistica.Persistence;
using Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.Instrumento.Views
{
    public partial class FindInstrumento : Page
    {
        //private readonly ManageInstrumento _managerInstrumento;
        private readonly ManageTipoEstadistica _managerTipoEstadistica;
        private readonly ManageEntidadFederativa _managerEntidadFederativa;
        private readonly ManageMunicipio _managerMunicipio;

        public FindInstrumento()
        {
            InitializeComponent();
            _managerTipoEstadistica = new ManageTipoEstadistica(new SqliteTipoEstadisticaRepository());
            _managerEntidadFederativa = new ManageEntidadFederativa(new SqliteEntidadFederativaRepository());
            _managerMunicipio =
                new ManageMunicipio(new SqliteMunicipioRepository(), new SqliteEntidadFederativaRepository());

            LoadForm();
        }

        private void LoadForm(object sender, RoutedEventArgs e)
        {
            LoadForm();
        }

        private void LoadComboBoxInstrumento(object sender, SelectionChangedEventArgs e)
        {
            cbxTipoInstrumento.Items.Clear();
            cbxTipoInstrumento.Items.Add("TODOS");
            if (cbxTipoEstadistica.SelectedItem is TipoEstadisticaResponse)
            {
                var tipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem);
                foreach (var instrumento in tipoEstadistica.Instrumentos)
                    cbxTipoInstrumento.Items.Add(instrumento);
            }

            cbxTipoInstrumento.SelectedIndex = 0;
        }

        private void LoadComboBoxMunicipio(object sender, SelectionChangedEventArgs e)
        {
            cbxMunicipio.Items.Clear();
            cbxMunicipio.Items.Add("TODOS");
            if (cbxEntidadFederativa.SelectedItem is EntidadFederativaResponse)
            {
                var entidadFederativa = (EntidadFederativaResponse)cbxEntidadFederativa.SelectedItem;
                var municipios = _managerMunicipio.SearchAllMunicipios(entidadFederativa.Id).Municipios;
                foreach (var municipio in municipios)
                    cbxMunicipio.Items.Add(municipio);
            }

            cbxMunicipio.SelectedIndex = 0;
        }

        private void ValidateConsecutivoFormat(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Find_PEC_6_60_Click(object sender, RoutedEventArgs e)
        {
            var finder = new SearchInstrumentoByCriteria(
                new SqliteInstrumentoRepository(),
                new SqliteMunicipioRepository(),
                new SqliteEntidadFederativaRepository(),
                new SqliteTipoEstadisticaRepository(),
                new SqliteTipoInstrumentoRepository()
            );

            if (cbxTipoEstadistica.SelectedItem is TipoEstadisticaResponse)
            {
                int idTipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem).Id;
                finder = finder.TipoEstadistica(idTipoEstadistica);
            }

            if (cbxTipoInstrumento.SelectedItem is TipoInstrumentoResponse)
            {
                int idTipoInstrumento = ((TipoInstrumentoResponse)cbxTipoInstrumento.SelectedItem).Id;
                finder = finder.TipoInstrumento(idTipoInstrumento);
            }

            if (int.TryParse(cbxAñoEstadistico.SelectedItem.ToString(), out _))
            {
                string añoEstadistico = cbxAñoEstadistico.SelectedItem.ToString();
                finder = finder.AñoEstadistico(añoEstadistico);
            }

            if (cbxMesEstadistico.SelectedItem is MesEstadistico)
            {
                int idMesEstadistico = ((MesEstadistico)cbxMesEstadistico.SelectedItem).Id;
                finder = finder.MesEstadistico(idMesEstadistico);
            }

            if (cbxEntidadFederativa.SelectedItem is EntidadFederativaResponse)
            {
                int idEntidadFederativa = ((EntidadFederativaResponse)cbxEntidadFederativa.SelectedItem).Id;
                finder = finder.EntidadFederativa(idEntidadFederativa);
            }

            if (cbxMunicipio.SelectedItem is MunicipioResponse)
            {
                int idMunicipio = ((MunicipioResponse)cbxMunicipio.SelectedItem).Id;
                finder = finder.Municipio(idMunicipio);
            }

            int consecutivo;
            if (int.TryParse(txtConsecutivo.Text, out consecutivo))
            {
                finder = finder.Consecutivo(consecutivo);
            }

            if (cbxSireso.SelectedValue.ToString() == "SI")
                finder.GuardadoSIRESO(true);

            if (cbxSireso.SelectedValue.ToString() == "NO")
                finder.GuardadoSIRESO(false);

            tblInstrumentos.ItemsSource = finder.SearchInstrumentos().Instrumentos;
        }

        private void UpdateStatusSIRESO_isChecked(object sender, RoutedEventArgs e)
        {
            var instrumento = ((InstrumentoData)((CheckBox)sender).DataContext);
            bool isChecked = ((CheckBox)sender).IsChecked.Value;
            if (isChecked)
            {
                new InstrumentoSavedInSireso(new SqliteInstrumentoRepository()).SavedInSIRESO(instrumento.Id);
            }
            else
            {
                MessageBoxResult resultado = MessageBox.Show(
                    Window.GetWindow(this),
                    $"¿Deseas marcar el instrumento {instrumento.Nombre} como no guardado en SIRESO?",
                    "Confirmación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (resultado == MessageBoxResult.Yes)
                    new InstrumentoSavedInSireso(new SqliteInstrumentoRepository()).UnsavedInSIRESO(instrumento.Id);
                else
                    ((CheckBox)e.Source).IsChecked = true;
            }
        }

        private void Show_PEC_6_60Details_Click(object sender, RoutedEventArgs e)
        {
            int idInstrumento = ((InstrumentoData)((Button)sender).DataContext).Id;
            var instrumento = new SearchInstrumentoById(
                new SqliteInstrumentoRepository(),
                new SqliteMunicipioRepository(),
                new SqliteEntidadFederativaRepository(),
                new SqliteTipoEstadisticaRepository(),
                new SqliteTipoInstrumentoRepository()
            ).SearchInstrumento(idInstrumento);
            ctrlPEC_6_60Details.LoadDetails(instrumento);
        }

        private void LoadForm()
        {
            var tiposEstadistica = _managerTipoEstadistica.SearchAllTiposEstadisitca().TiposEstadistica;
            cbxTipoEstadistica.Items.Clear();
            cbxTipoEstadistica.Items.Add("TODOS");
            foreach (var tipoEstadistica in tiposEstadistica)
                cbxTipoEstadistica.Items.Add(tipoEstadistica);
            cbxTipoEstadistica.SelectedIndex = 0;

            cbxTipoInstrumento.Items.Clear();
            cbxTipoInstrumento.Items.Add("TODOS");
            cbxTipoInstrumento.SelectedIndex = 0;

            IEnumerable<int> years = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1);
            cbxAñoEstadistico.Items.Clear();
            cbxAñoEstadistico.Items.Add("TODOS");
            foreach (int year in years)
                cbxAñoEstadistico.Items.Add(year);
            cbxAñoEstadistico.SelectedIndex = 0;

            var months = MesEstadistico.Meses;
            cbxMesEstadistico.Items.Clear();
            cbxMesEstadistico.Items.Add("TODOS");
            foreach (var month in months)
                cbxMesEstadistico.Items.Add(month);
            cbxMesEstadistico.SelectedIndex = 0;

            var entidadesFederativas = _managerEntidadFederativa.SearchAllEntidadesFederativas().EntidadesFederativas;
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
    }
}