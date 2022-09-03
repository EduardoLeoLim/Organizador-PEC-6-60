using System.IO;

namespace Organizador_PEC_6_60.Application.Instrumento.Export
{
    public class PdfInstrumentoExporter : InstrumentoExporter
    {
        public string Export(InstrumentoData instrumento, string dirPath)
        {
            string filePath = dirPath + instrumento.Nombre + ".pdf";
            MemoryStream memoryStream = new MemoryStream(instrumento.Archivo);
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            memoryStream.WriteTo(file);
            file.Close();
            memoryStream.Close();
            return filePath;
        }
    }
}

