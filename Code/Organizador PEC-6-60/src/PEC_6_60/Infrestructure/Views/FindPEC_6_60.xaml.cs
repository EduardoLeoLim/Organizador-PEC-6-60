using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.EntidadFederativa.Application;
using Organizador_PEC_6_60.EntidadFederativa.Infrestructure.Persistence;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Infrestructure.Persistence;
using Organizador_PEC_6_60.Municipio.Application;
using Organizador_PEC_6_60.Municipio.Infrestructure.Persistence;
using Organizador_PEC_6_60.PEC_6_60.Application;
using Organizador_PEC_6_60.PEC_6_60.Application.Search;
using Organizador_PEC_6_60.PEC_6_60.Infrestructure.Persistence;
using Organizador_PEC_6_60.TipoEstadistica.Application;
using Organizador_PEC_6_60.TipoEstadistica.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.PEC_6_60.Infrestructure.Views
{
    public partial class FindPEC_6_60 : Page
    {
        private readonly ManagePEC_6_60 _managerPEC_6_60;
        private readonly ManageTipoEstadistica _managerTipoEstadistica;
        private readonly ManageEntidadFederativa _managerEntidadFederativa;
        private readonly ManageMunicipio _managerMunicipio;

        public FindPEC_6_60()
        {
            InitializeComponent();
            _managerPEC_6_60 = new ManagePEC_6_60(new SqlitePEC_6_60Repository(), new SqliteInstrumentoRepository(),
                new SqliteTipoEstadisticaRepository(), new SqliteEntidadFederativaRepository(),
                new SqliteMunicipioRepository());
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
            cbxInstrumento.Items.Clear();
            cbxInstrumento.Items.Add("TODOS");
            if (cbxTipoEstadistica.SelectedItem is TipoEstadisticaResponse)
            {
                var tipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem);
                foreach (var instrumento in tipoEstadistica.Instrumentos)
                    cbxInstrumento.Items.Add(instrumento);
            }

            cbxInstrumento.SelectedIndex = 0;
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
            int idTipoEstadistica = cbxTipoEstadistica.SelectedItem is TipoEstadisticaResponse tipoEstadistica
                ? tipoEstadistica.Id
                : 0;

            int idInstrumento = cbxInstrumento.SelectedItem is InstrumentoResponse instrumento
                ? instrumento.Id
                : 0;

            int añoSeleccionado;
            string añoEstadistico = int.TryParse(cbxAñoEstadistico.SelectedItem.ToString(), out añoSeleccionado)
                ? añoSeleccionado.ToString()
                : "";

            int idMesEstadistico = cbxMesEstadistico.SelectedItem is MesEstadistico mesEstadistico
                ? mesEstadistico.Id
                : 0;

            int idEntidadFederativa = cbxEntidadFederativa.SelectedItem is EntidadFederativaResponse entidadFederativa
                ? entidadFederativa.Id
                : 0;

            int idMunicipio = cbxMunicipio.SelectedItem is MunicipioResponse municipio
                ? municipio.Id
                : 0;

            int consecutivo = int.TryParse(txtConsecutivo.Text, out _) ? int.Parse(txtConsecutivo.Text) : 0;

            FilterSIRESO guardadoSIRESO = cbxSireso.SelectedItem is FilterSIRESO filter ? filter : FilterSIRESO.TODOS;

            tblInstrumentos.ItemsSource = _managerPEC_6_60
                .SearchPEC_6_60ByCriteria(
                    idTipoEstadistica: idTipoEstadistica,
                    idInstrumento: idInstrumento,
                    añoEstadistico: añoEstadistico,
                    mesEstadistico: idMesEstadistico,
                    idEntidadFederativa: idEntidadFederativa,
                    idMunicipio: idMunicipio,
                    consecutivo: consecutivo,
                    guardadoSIRESO: guardadoSIRESO
                ).PEC_6_60s;
        }

        private void LoadForm()
        {
            var tiposEstadistica = _managerTipoEstadistica.SearchAllTiposEstadisitca().TiposEstadistica;
            cbxTipoEstadistica.Items.Clear();
            cbxTipoEstadistica.Items.Add("TODOS");
            foreach (var tipoEstadistica in tiposEstadistica)
                cbxTipoEstadistica.Items.Add(tipoEstadistica);
            cbxTipoEstadistica.SelectedIndex = 0;

            cbxInstrumento.Items.Clear();
            cbxInstrumento.Items.Add("TODOS");
            cbxInstrumento.SelectedIndex = 0;

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
            cbxSireso.Items.Add(FilterSIRESO.TODOS);
            cbxSireso.Items.Add(FilterSIRESO.SI);
            cbxSireso.Items.Add(FilterSIRESO.NO);
            cbxSireso.SelectedIndex = 0;
        }
    }
}