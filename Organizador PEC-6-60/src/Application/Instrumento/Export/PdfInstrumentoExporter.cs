using System.IO;

namespace Organizador_PEC_6_60.Application.Instrumento.Export;

public class PdfInstrumentoExporter : InstrumentoExporter
{
    public string Export(InstrumentoData instrumento, string dirPath)
    {
        var filePath = dirPath + instrumento.Nombre + ".pdf";
        var memoryStream = new MemoryStream(instrumento.Archivo);
        var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        memoryStream.WriteTo(file);
        file.Close();
        memoryStream.Close();
        return filePath;
    }
}