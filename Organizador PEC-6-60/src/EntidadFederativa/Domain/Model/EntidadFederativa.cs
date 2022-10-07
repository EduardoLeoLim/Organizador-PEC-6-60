using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;

namespace Organizador_PEC_6_60.EntidadFederativa.Domain.Model;

public class EntidadFederativa
{
    public EntidadFederativa(EntidadFederativaClave clave, EntidadFederativaNombre nombre, int id = 0)
    {
        Id = id;
        Clave = clave;
        Nombre = nombre;
    }

    public int Id { get; }
    public EntidadFederativaClave Clave { get; }
    public EntidadFederativaNombre Nombre { get; }
}