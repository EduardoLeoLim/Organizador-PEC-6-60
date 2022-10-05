namespace Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;

public class InstrumentoAñoEstadistico
{
    public InstrumentoAñoEstadistico(string añoEstadistico)
    {
        Value = añoEstadistico;
    }

    public string Value { get; }
}