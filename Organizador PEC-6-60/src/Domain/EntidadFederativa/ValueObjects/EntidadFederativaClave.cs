namespace Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

public class EntidadFederativaClave
{
    public EntidadFederativaClave(int clave)
    {
        Value = clave;
    }

    public int Value { get; }
}