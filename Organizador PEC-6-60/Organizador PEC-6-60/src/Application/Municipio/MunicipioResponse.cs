using Organizador_PEC_6_60.Application.EntidadFederativa;

namespace Organizador_PEC_6_60.Application.Municipio
{
    public class MunicipioResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public EntidadFederativaResponse EntidadFederativa { get; }

        public MunicipioResponse(
            int id,
            int clave,
            string nombre,
            EntidadFederativaResponse entidadFederativa
        )
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            EntidadFederativa = entidadFederativa;
        }

        public static MunicipioResponse FromAggregate(
            Domain.Municipio.Model.Municipio municipio,
            Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa
        )
        {
            EntidadFederativaResponse entidadFederativaResponse =
                EntidadFederativaResponse.FromAggregate(entidadFederativa);

            return new MunicipioResponse(
                municipio.Id,
                municipio.Clave.Value,
                municipio.Nombre.Value,
                entidadFederativaResponse
            );
        }

        public override string ToString()
        {
            return $"{Clave:000} - {Nombre}";
        }
    }
}