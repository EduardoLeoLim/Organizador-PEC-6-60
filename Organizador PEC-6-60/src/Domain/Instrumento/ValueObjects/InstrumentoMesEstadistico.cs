namespace Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;

public class InstrumentoMesEstadistico
{
    public InstrumentoMesEstadistico(int mesEstadistico)
    {
        Value = mesEstadistico;
    }

    public int Value { get; }
}