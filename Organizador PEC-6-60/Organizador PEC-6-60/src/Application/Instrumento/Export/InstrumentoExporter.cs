namespace Organizador_PEC_6_60.Application.Instrumento.Export;

public interface InstrumentoExporter
{
    public string Export(InstrumentoData instrumento, string dirPath);
}