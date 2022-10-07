using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Application.Export;

namespace Organizador_PEC_6_60.Instrumento.Infrastructure.Views;

public partial class ShowInstrumentoDetails : UserControl
{
    private InstrumentoData _instrumento;

    public ShowInstrumentoDetails()
    {
        InitializeComponent();
    }

    private void Hide_PEC_6_60Details_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Hidden;
    }

    private void LoadComboBoxInstrumento(object sender, SelectionChangedEventArgs e)
    {
        //throw new System.NotImplementedException();
    }

    private void LoadComboBoxMunicipio(object sender, SelectionChangedEventArgs e)
    {
        //throw new System.NotImplementedException();
    }

    private void ValidateConsecutivoFormat(object sender, TextCompositionEventArgs e)
    {
        //throw new System.NotImplementedException();
    }

    private void CopyPDF_Click(object sender, RoutedEventArgs e)
    {
        var wasExported =
            new ExportInstrumento(new PdfInstrumentoExporter()).Export(_instrumento, Path.GetTempPath());

        if (wasExported)
            MessageBox.Show(
                "Ruta del archivo copiada al portapapeles",
                "Instrumento copiado",
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk
            );
        else
            MessageBox.Show(
                "Error al exportar el instrumento, intentalo de nuevo.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
    }

    public void LoadDetails(InstrumentoData pec660)
    {
        _instrumento = pec660;
        lblNombrePEC_6_60.Text = $"PEC-6-60 - {_instrumento.Nombre}";
        pdfViewer.Load(new MemoryStream(_instrumento.Archivo));

        Visibility = Visibility.Visible;
    }
}