using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Application.Create;

public interface InstrumentoCreatorService
{
    void Create(InstrumentoAñoEstadistico añoEstadistico, InstrumentoMesEstadistico mesEstadistico,
        InstrumentoConsecutivo consecutivo, byte[] dataArchivo, int idInstrumento, int idTipoEstadistica,
        int idMunicipio);
}