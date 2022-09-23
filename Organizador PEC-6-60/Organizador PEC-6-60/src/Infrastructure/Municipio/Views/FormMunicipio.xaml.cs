using System;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio;
using Organizador_PEC_6_60.Domain.Municipio.Exceptions;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.Municipio.Views
{
    public partial class FormMunicipio : Window
    {
        private readonly ManageMunicipio _managerMunicipios;
        private bool isNewRecord;
        private MunicipioResponse _municipio;

        public FormMunicipio(ManageMunicipio managetMunicipios)
        {
            InitializeComponent();
            _managerMunicipios = managetMunicipios;
            isNewRecord = true;
            SearchAllEntidadesFederativas allEntidadesFederativasSearcher =
                new SearchAllEntidadesFederativas(
                    new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
                );
            var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
            cbxEntidadFederativa.ItemsSource = entidadesFederativas;
        }

        public FormMunicipio(ManageMunicipio managetMunicipios, int idMunicipio) : this(managetMunicipios)
        {
            isNewRecord = false;
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
                    DataEntidadFederativa dataEntidadFederativaSeleccionada =
                        (DataEntidadFederativa)cbxEntidadFederativa.SelectionBoxItem;

                    if (isNewRecord)
                    {
                        _managerMunicipios.RegisterMunicipio(
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
                        _managerMunicipios.UpdateMunicipio(
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
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadForm(int idMunicipio)
        {
            try
            {
                _municipio = _managerMunicipios.SearchMunicipioById(idMunicipio);
                txtClave.Text = _municipio.Clave.ToString();
                txtNombre.Text = _municipio.Nombre;
                var entidades = cbxEntidadFederativa.ItemsSource.Cast<DataEntidadFederativa>();
                int indexEntidadFederativa =
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
            bool isThere = false;

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
}