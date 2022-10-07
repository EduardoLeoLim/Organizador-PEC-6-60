using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public class DataMunicipios
{
    public DataMunicipios(
        IEnumerable<Domain.Model.Municipio> municipios,
        EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa
    )
    {
        Municipios = municipios.Select(row => DataMunicipio.FromAggregate(row, entidadFederativa));
    }

    public IEnumerable<DataMunicipio> Municipios { get; }
}