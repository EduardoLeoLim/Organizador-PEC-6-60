namespace Organizador_PEC_6_60.Instrumento.Application.Export;

public interface InstrumentoExporter
{
    public string Export(InstrumentoData instrumento, string dirPath);
}