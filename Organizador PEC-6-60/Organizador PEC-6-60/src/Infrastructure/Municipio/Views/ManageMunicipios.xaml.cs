﻿using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;
using Organizador_PEC_6_60.Infrastructure.Municipio.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.Municipio.Views
{
    public partial class ManageMunicipios : Page
    {
        private readonly ManageMunicipio _managerMunicipios;

        public ManageMunicipios()
        {
            InitializeComponent();
            _managerMunicipios =
                new ManageMunicipio(SqliteMunicipioRepository.Instance, SqliteEntidadFederativaRepository.Instance);
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void FindMunicipios_Click(object sender, RoutedEventArgs e)
        {
            if (cbxEntidadFederativa.SelectedIndex >= 0)
            {
                DataEntidadFederativa dataEntidadFederativaSelecionada =
                    (DataEntidadFederativa)cbxEntidadFederativa.SelectionBoxItem;
                var municipios = _managerMunicipios.SearchAllMunicipios(dataEntidadFederativaSelecionada.Id).Municipios;
                tblMunicipios.ItemsSource = municipios;
            }
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormMunicipio form = new FormMunicipio(_managerMunicipios);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadPage();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            MunicipioResponse record = (MunicipioResponse)((Button)e.Source).DataContext;
            FormMunicipio form = new FormMunicipio(_managerMunicipios, record.Id);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadPage();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            MunicipioResponse record = (MunicipioResponse)((Button)e.Source).DataContext;
            string message = "¿Quiere eliminar el registro?";
            message += $"\nClave: {record.Clave}";
            message += $"\nNombre: {record.Nombre}";

            MessageBoxResult result = MessageBox.Show(
                message,
                "Eliminar",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No
            );

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _managerMunicipios.DeleteMunicipio(record.Id);
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

                LoadPage();
            }
        }

        private void LoadPage()
        {
            SearchAllEntidadesFederativas allEntidadesFederativasSearcher =
                new SearchAllEntidadesFederativas(
                    new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
                );
            var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
            cbxEntidadFederativa.ItemsSource = entidadesFederativas;
            tblMunicipios.ItemsSource = null;
        }
    }
}