namespace Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

public class EntidadFederativaNombre
{
    public EntidadFederativaNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}