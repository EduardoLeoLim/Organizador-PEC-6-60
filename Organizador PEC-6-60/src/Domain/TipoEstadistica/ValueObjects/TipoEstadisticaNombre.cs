namespace Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;

public class TipoEstadisticaNombre
{
    public TipoEstadisticaNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}