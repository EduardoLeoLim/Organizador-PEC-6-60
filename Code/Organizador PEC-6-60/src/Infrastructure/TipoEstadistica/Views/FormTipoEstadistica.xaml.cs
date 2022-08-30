using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.TipoEstadistica;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Exceptions;
using Organizador_PEC_6_60.Instrumento.Application;

namespace Organizador_PEC_6_60.Infrastructure.TipoEstadistica.Views
{
    public partial class FormTipoEstadistica : Window
    {
        private ManageTipoEstadistica _managerTipoEstadistica;
        private ManageTiposInstrumento _managerTiposInstrumento;
        private bool isNewRecord;
        private TipoEstadisticaResponse _tipoEstadistica;

        public FormTipoEstadistica(ManageTipoEstadistica managerTipoEstadistica, ManageTiposInstrumento managerTiposInstrumento)
        {
            InitializeComponent();
            _managerTipoEstadistica = managerTipoEstadistica;
            _managerTiposInstrumento = managerTiposInstrumento;
            isNewRecord = true;
            LoadForm();
        }

        public FormTipoEstadistica(ManageTipoEstadistica managerTipoEstadistica, ManageTiposInstrumento managerTiposInstrumento,
            int idTipoEstadistica) : this(managerTipoEstadistica, managerTiposInstrumento)
        {
            isNewRecord = false;
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
                    if (isNewRecord)
                    {
                        _managerTipoEstadistica.RegisterTipoEstadistica(int.Parse(txtClave.Text), txtNombre.Text,
                            GetSelectedInstrumentos());
                        MessageBox.Show("Tipo de estadística registrada.", "Exito", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        _managerTipoEstadistica.UpdateTipoEstadistica(_tipoEstadistica.Id, int.Parse(txtClave.Text),
                            txtNombre.Text,
                            GetSelectedInstrumentos());
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

        private void LoadForm()
        {
            try
            {
                var instrumentos = _managerTiposInstrumento.SearchAllInstrumentos().TiposInstrumento;
                foreach (var instrumento in instrumentos)
                {
                    CheckBox item = new CheckBox();
                    item.DataContext = instrumento;
                    item.SetBinding(CheckBox.ContentProperty, "Nombre");
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
                _tipoEstadistica = _managerTipoEstadistica.SearchTipoEstadisticaById(idTipoEstadisitica);
                txtClave.Text = _tipoEstadistica.Clave.ToString();
                txtNombre.Text = _tipoEstadistica.Nombre;

                foreach (var child in listInstrumentos.Children)
                {
                    if (child is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)child;
                        var instrumento = (TipoInstrumentoResponse)checkBox.DataContext;
                        int count = _tipoEstadistica.Instrumentos.Where(item => item.Id == instrumento.Id).Count();
                        if (count == 1)
                        {
                            checkBox.IsChecked = true;
                        }
                    }
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
            List<TipoInstrumentoResponse> listSelectedInstrumentos = new List<TipoInstrumentoResponse>();

            foreach (var item in listInstrumentos.Children)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)item;
                    if (checkBox.IsChecked == true)
                    {
                        TipoInstrumentoResponse instrumento = (TipoInstrumentoResponse)checkBox.DataContext;
                        listSelectedInstrumentos.Add(instrumento);
                    }
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

            return isThere;
        }
    }
}