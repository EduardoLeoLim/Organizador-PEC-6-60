﻿using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Organizador_PEC_6_60.Application.EntidadFederativa.Delete;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence;

namespace Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Views
{
    public partial class ManageEntidadesFederativas : Page
    {
        public ManageEntidadesFederativas()
        {
            InitializeComponent();
            LoadTable();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            FormEntidadFederativa form = new FormEntidadFederativa();
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            DataEntidadFederativa record = (DataEntidadFederativa)((Button)e.Source).DataContext;
            FormEntidadFederativa form = new FormEntidadFederativa(record.Id);
            form.Owner = Window.GetWindow(this);
            form.ShowDialog();
            LoadTable();
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            DataEntidadFederativa record = (DataEntidadFederativa)((Button)e.Source).DataContext;
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
                    EntidadFederativaDeleterService _deleter =
                        new EntidadFederativaDeleter(SqliteEntidadFederativaRepository.Instance);
                    _deleter.Delete(record.Id);
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

                LoadTable();
            }
        }

        private void LoadTable()
        {
            try
            {
                SearchAllEntidadesFederativas allEntidadesFederativasSearcher =
                    new SearchAllEntidadesFederativas(
                        new EntidadFederativaAllSearcher(SqliteEntidadFederativaRepository.Instance)
                    );
                var entidadesFederativas = allEntidadesFederativasSearcher.SearchAll().EntidadesFederativas;
                tblEntidades.ItemsSource = entidadesFederativas;
            }
            catch (DbException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error base de datos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                tblEntidades.Items.Clear();
            }
        }
    }
}