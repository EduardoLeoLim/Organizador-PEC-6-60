namespace Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

public class MunicipioNombre
{
    public MunicipioNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}