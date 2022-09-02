using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Organizador_PEC_6_60.Application.Instrumento;

namespace Organizador_PEC_6_60.Infrastructure.Instrumento.Views
{
    public partial class ShowInstrumentoDetails : UserControl
    {
        private InstrumentoData _pec660;

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
            string filePath = Path.GetTempPath() + _pec660.Nombre + ".pdf";
            MemoryStream memoryStream = new MemoryStream(_pec660.Archivo);
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            memoryStream.WriteTo(file);
            file.Close();
            memoryStream.Close();
            Clipboard.SetText(filePath);

            MessageBox.Show(filePath);
        }

        public void LoadDetails(InstrumentoData pec660)
        {
            _pec660 = pec660;
            lblNombrePEC_6_60.Text = $"PEC-6-60 - {_pec660.Nombre}";
            pdfViewer.Load(new MemoryStream(_pec660.Archivo));

            Visibility = Visibility.Visible;
        }
    }
}