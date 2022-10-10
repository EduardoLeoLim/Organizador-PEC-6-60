using System.IO;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Instrumento.Domain.Services;

namespace Organizador_PEC_6_60.Instrumento.Infrastructure.Export;

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