namespace Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

public class TipoInstrumentoNombre
{
    public TipoInstrumentoNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}