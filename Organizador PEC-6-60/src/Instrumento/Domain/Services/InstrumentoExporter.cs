using Organizador_PEC_6_60.Instrumento.Application;

namespace Organizador_PEC_6_60.Instrumento.Domain.Services;

public interface InstrumentoExporter
{
    public string Export(InstrumentoData instrumento, string dirPath);
}