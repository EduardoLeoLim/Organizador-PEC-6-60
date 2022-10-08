using Organizador_PEC_6_60.EntidadFederativa.Application.Search;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public class DataMunicipio
{
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

    public int Id { get; }
    public int Clave { get; }
    public string Nombre { get; }
    public DataEntidadFederativa DataEntidadFederativa { get; }

    public static DataMunicipio FromAggregate(
        Domain.Model.Municipio municipio,
        EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa
    )
    {
        var dataEntidadFederativa = DataEntidadFederativa.FromAggregate(entidadFederativa);

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