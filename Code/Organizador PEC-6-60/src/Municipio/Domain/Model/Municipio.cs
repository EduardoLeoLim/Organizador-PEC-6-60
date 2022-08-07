using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Domain.Model
{
    public class Municipio
    {
        #region Properties

        public int Id { get; }
        public MunicipioClave Clave { get; }
        public MunicipioNombre Nombre { get; }
        public EntidadFederativa.Domain.Model.EntidadFederativa EntidadFederativa { get; }

        #endregion

        #region Constructors

        public Municipio(MunicipioClave clave, MunicipioNombre nombre,
            EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa, int id = 0)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            EntidadFederativa = entidadFederativa;
        }

        #endregion
    }
}