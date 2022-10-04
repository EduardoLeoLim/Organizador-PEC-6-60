using Organizador_PEC_6_60.Application.EntidadFederativa.Search;

namespace Organizador_PEC_6_60.Application.Municipio.Search
{
    public class DataMunicipio
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public DataEntidadFederativa DataEntidadFederativa { get; }

        public DataMunicipio(
            int id,
            int clave,
            string nombre,
            DataEntidadFederativa dataEntidadFederativa
        )
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            DataEntidadFederativa = dataEntidadFederativa;
        }

        public static DataMunicipio FromAggregate(
            Domain.Municipio.Model.Municipio municipio,
            Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa
        )
        {
            DataEntidadFederativa dataEntidadFederativa =
                DataEntidadFederativa.FromAggregate(entidadFederativa);

            return new DataMunicipio(
                municipio.Id,
                municipio.Clave.Value,
                municipio.Nombre.Value,
                dataEntidadFederativa
            );
        }

        public override string ToString()
        {
            return $"{Clave:000} - {Nombre}";
        }
    }
}