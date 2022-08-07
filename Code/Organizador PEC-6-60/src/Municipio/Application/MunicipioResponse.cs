using Organizador_PEC_6_60.EntidadFederativa.Application;

namespace Organizador_PEC_6_60.Municipio.Application
{
    public class MunicipioResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public EntidadFederativa.Application.EntidadFederativaResponse EntidadFederativa { get; }

        public MunicipioResponse(int id, int clave, string nombre,
            EntidadFederativa.Application.EntidadFederativaResponse entidadFederativa)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            EntidadFederativa = entidadFederativa;
        }

        public static MunicipioResponse FromAggregate(Domain.Model.Municipio municipio)
        {
            EntidadFederativaResponse entidadFederativa =
                EntidadFederativaResponse.FromAggregate(municipio.EntidadFederativa);
            return new MunicipioResponse(municipio.Id, municipio.Clave.Value, municipio.Nombre.Value,
                entidadFederativa);
        }
    }
}