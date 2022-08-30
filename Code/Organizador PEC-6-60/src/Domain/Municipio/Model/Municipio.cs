using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Domain.Municipio.Model
{
    public class Municipio
    {
        #region Properties

        public int Id { get; }
        public MunicipioClave Clave { get; }
        public MunicipioNombre Nombre { get; }
        public int IdEntidadFederativa { get; }

        #endregion

        #region Constructors

        public Municipio(MunicipioClave clave, MunicipioNombre nombre, int idEntidadFederativa, int id = 0)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            IdEntidadFederativa = idEntidadFederativa;
        }

        #endregion
    }
}