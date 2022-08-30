using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Application.EntidadFederativa;
using Organizador_PEC_6_60.Application.Municipio;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;
using Organizador_PEC_6_60.Infrastructure.Municipio.Persistence;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Infrestructure.Persistence;
using Organizador_PEC_6_60.Municipio.Application;
using Organizador_PEC_6_60.PEC_6_60.Application;
using Organizador_PEC_6_60.PEC_6_60.Infrestructure.Persistence;
using Organizador_PEC_6_60.TipoEstadistica.Application;
using Organizador_PEC_6_60.TipoEstadistica.Infrestructure.Persistence;

namespace Organizador_PEC_6_60.PEC_6_60.Infrestructure.Views
{
    public partial class SavePEC_6_60 : Page
    {
        private readonly ManagePEC_6_60 _managerPEC_6_60;
        private readonly ManageTipoEstadistica _managerTipoEstadistica;
        private readonly ManageEntidadFederativa _managerEntidadFederativa;
        private readonly ManageMunicipio _managerMunicipio;

        public SavePEC_6_60()
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
            if (cbxTipoEstadistica.SelectedIndex >= 0)
            {
                var tipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem);
                cbxInstrumento.ItemsSource = tipoEstadistica.Instrumentos;
            }
        }

        private void LoadComboBoxMunicipio(object sender, SelectionChangedEventArgs e)
        {
            if (cbxEntidadFederativa.SelectedIndex >= 0)
            {
                var entidadFederativa = (EntidadFederativaResponse)cbxEntidadFederativa.SelectedItem;
                cbxMunicipio.ItemsSource = _managerMunicipio.SearchAllMunicipios(entidadFederativa.Id).Municipios;
            }
        }

        private void SelectFile(object sender, EventArgs args)
        {
            lblFilePath.Text = pdfViewer.DocumentInfo.FilePath + pdfViewer.DocumentInfo.FileName;
        }

        private void ValidateConsecutivoFormat(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
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
                    int idTipoEstadistica = ((TipoEstadisticaResponse)cbxTipoEstadistica.SelectedItem).Id;
                    int idInstrumento = ((InstrumentoResponse)cbxInstrumento.SelectedItem).Id;
                    int idMunicipio = ((MunicipioResponse)cbxMunicipio.SelectedItem).Id;
                    string añoEstadistico = cbxAñoEstadistico.SelectedValue.ToString();
                    int mesEstadistico = (int)((dynamic)cbxMesEstadistico.SelectedItem).Id;
                    int consecutivo = int.Parse(txtConsecutivo.Text);

                    MemoryStream memoryStream = new MemoryStream();
                    pdfViewer.LoadedDocument.Save(memoryStream);
                    byte[] dataArchivo = memoryStream.ToArray();

                    _managerPEC_6_60.RegisterPEC_6_60(idTipoEstadistica, idInstrumento, idMunicipio, añoEstadistico,
                        mesEstadistico, consecutivo, dataArchivo);
                    MessageBox.Show("PEC-6-60 registrado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
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
            cbxEntidadFederativa.ItemsSource =
                _managerEntidadFederativa.SearchAllEntidadesFederativas().EntidadesFederativas;
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
            Style textBoxStyle = System.Windows.Application.Current.TryFindResource(typeof(TextBox)) as Style;
            Style comboBoxStyle = System.Windows.Application.Current.TryFindResource(typeof(ComboBox)) as Style;

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
            bool isThere = false;

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

            if (pdfViewer.LoadedDocument == null)
            {
                isThere = true;
            }

            return isThere;
        }
    }
}