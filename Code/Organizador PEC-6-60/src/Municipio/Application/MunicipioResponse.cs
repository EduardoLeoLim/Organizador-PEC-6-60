using Organizador_PEC_6_60.EntidadFederativa.Application;

namespace Organizador_PEC_6_60.Municipio.Application
{
    public class MunicipioResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public EntidadFederativaResponse EntidadFederativa { get; }

        public MunicipioResponse(int id, int clave, string nombre,
            EntidadFederativaResponse entidadFederativa)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            EntidadFederativa = entidadFederativa;
        }

        public static MunicipioResponse FromAggregate(Domain.Model.Municipio municipio,
            EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa)
        {
            EntidadFederativaResponse entidadFederativaResponse = EntidadFederativaResponse.FromAggregate(entidadFederativa);
            return new MunicipioResponse(municipio.Id, municipio.Clave.Value, municipio.Nombre.Value,
                entidadFederativaResponse);
        }

        public override string ToString()
        {
            return $"{Clave:000} - {Nombre}";
        }
    }
}