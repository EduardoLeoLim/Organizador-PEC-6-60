using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Domain.Municipio.Model;

public class Municipio
{
    public Municipio(MunicipioClave clave, MunicipioNombre nombre, int idEntidadFederativa, int id = 0)
    {
        Id = id;
        Clave = clave;
        Nombre = nombre;
        IdEntidadFederativa = idEntidadFederativa;
    }

    public int Id { get; }
    public MunicipioClave Clave { get; }
    public MunicipioNombre Nombre { get; }
    public int IdEntidadFederativa { get; }
}