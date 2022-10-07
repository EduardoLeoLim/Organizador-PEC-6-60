namespace Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

public class TipoInstrumentoNombre
{
    public TipoInstrumentoNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}