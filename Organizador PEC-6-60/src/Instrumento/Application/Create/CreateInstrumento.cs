using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Application.Create;

public class CreateInstrumento
{
    private readonly InstrumentoCreatorService _creator;

    public CreateInstrumento(InstrumentoCreatorService creator)
    {
        _creator = creator;
    }

    public void CreateNewInstrumento(
        int idTipoEstadistica,
        int idInstrumento,
        int idMunicipio,
        string añoEstadistico,
        int mesEstadistico,
        int consecutivo,
        byte[] dataArchivo
    )
    {
        _creator.Create(
            new InstrumentoAñoEstadistico(añoEstadistico),
            new InstrumentoMesEstadistico(mesEstadistico),
            new InstrumentoConsecutivo(consecutivo),
            dataArchivo,
            idInstrumento,
            idTipoEstadistica,
            idMunicipio
        );
    }
}