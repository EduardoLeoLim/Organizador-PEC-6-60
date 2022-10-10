using System.IO;
using System.Windows;
using Organizador_PEC_6_60.Instrumento.Domain.Services;

namespace Organizador_PEC_6_60.Instrumento.Application.Export;

public class ExportInstrumento
{
    private readonly InstrumentoExporter _exporter;

    public ExportInstrumento(InstrumentoExporter exporter)
    {
        _exporter = exporter;
    }

    public bool Export(InstrumentoData instrumento, string dirPath)
    {
        var fileAttributes = File.GetAttributes(dirPath);
        if (fileAttributes.HasFlag(FileAttributes.Directory))
        {
            var path = _exporter.Export(instrumento, dirPath);
            Clipboard.SetText(path);
            return true;
        }

        return false;
    }
}