﻿using System;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Exceptions;
using Organizador_PEC_6_60.Instrumento.Application;

namespace Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Views
{
    public partial class FormInstrumento : Window
    {
        private readonly ManageTiposInstrumento _managerTiposInstrumentos;
        private bool isNewRecord;
        private TipoInstrumentoResponse _instrumento;

        public FormInstrumento(ManageTiposInstrumento managerTiposInstrumentos)
        {
            InitializeComponent();
            _managerTiposInstrumentos = managerTiposInstrumentos;
            isNewRecord = true;
        }

        public FormInstrumento(ManageTiposInstrumento managerTiposInstrumentos, int idInstrumento) : this(managerTiposInstrumentos)
        {
            isNewRecord = false;
            LoadForm(idInstrumento);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtNombre.IsEnabled = false;
                btnSave.IsEnabled = false;

                if (IsValidFormData())
                {
                    if (isNewRecord)
                    {
                        _managerTiposInstrumentos.RegisterInstrumento(txtNombre.Text);
                        MessageBox.Show("TipoInstrumento registrado.", "Exito", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        _managerTiposInstrumentos.UpdateInstrumento(_instrumento.Id, txtNombre.Text);
                        MessageBox.Show("TipoInstrumento editado.", "Exito", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                }
            }
            catch (InvalidNombreTipoInstrumento ex)
            {
                txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
                MessageBox.Show(ex.Message, "Error Nombre", MessageBoxButton.OK, MessageBoxImage.Error);
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
                txtNombre.IsEnabled = true;
                btnSave.IsEnabled = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadForm(int idInstrumento)
        {
            try
            {
                _instrumento = _managerTiposInstrumentos.SearchInstrumentoById(idInstrumento);
                txtNombre.Text = _instrumento.Nombre;
            }
            catch (DbException ex)
            {
                btnSave.IsEnabled = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidFormData()
        {
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

            if (txtNombre.Text.Length == 0)
            {
                txtNombre.Style = System.Windows.Application.Current.FindResource("has-error") as Style;
                isThere = true;
            }

            return isThere;
        }
    }
}