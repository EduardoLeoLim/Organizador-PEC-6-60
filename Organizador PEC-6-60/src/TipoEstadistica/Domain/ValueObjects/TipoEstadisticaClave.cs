namespace Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

public class TipoEstadisticaClave
{
    public TipoEstadisticaClave(int clave)
    {
        Value = clave;
    }

    public int Value { get; }
}