using System.IO;
using System.Windows;

namespace Organizador_PEC_6_60.Application.Instrumento.Export;

public class ExportInstrumento
{
    private readonly InstrumentoExporter _exporter;

    public ExportInstrumento(InstrumentoExporter exporter)
    {
        _exporter = exporter;
    }

    public bool Export(InstrumentoData instrumento, string dirPath)
    {
        FileAttributes fileAttributes = File.GetAttributes(dirPath);
        if (fileAttributes.HasFlag(FileAttributes.Directory))
        {
            string path = _exporter.Export(instrumento, dirPath);
            Clipboard.SetText(path);
            return true;
        }

        return false;
    }
}