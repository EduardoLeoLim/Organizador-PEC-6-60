using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.PEC_6_60.Application;

namespace Organizador_PEC_6_60.PEC_6_60.Infrestructure.Views;

public partial class ShowPEC_6_60Details : UserControl
{
    private PEC_6_60Response _pec660;
    
    public ShowPEC_6_60Details()
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
        string filePath = Path.GetTempPath() + _pec660.Nombre + ".pdf";
        MemoryStream memoryStream = new MemoryStream(_pec660.Archivo);
        FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        memoryStream.WriteTo(file);
        file.Close();
        memoryStream.Close();
        Clipboard.SetText(filePath);
        
        MessageBox.Show(filePath);
    }
    
    public void LoadDetails(PEC_6_60Response pec660)
    {
        _pec660 = pec660;
        lblNombrePEC_6_60.Text = $"PEC-6-60 - {_pec660.Nombre}";
        pdfViewer.Load(new MemoryStream(_pec660.Archivo));

        Visibility = Visibility.Visible;
    }
}