using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Domain.EntidadFederativa.Model
{
    public class EntidadFederativa
    {
        public int Id { get; }
        public EntidadFederativaClave Clave { get; }
        public EntidadFederativaNombre Nombre { get; }

        public EntidadFederativa(EntidadFederativaClave clave, EntidadFederativaNombre nombre, int id = 0)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
        }
    }
}