namespace Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

public class MunicipioNombre
{
    public MunicipioNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}