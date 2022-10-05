using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.Municipio.Search;

public class DataMunicipios
{
    public DataMunicipios(
        IEnumerable<Domain.Municipio.Model.Municipio> municipios,
        Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa
    )
    {
        Municipios = municipios.Select(row => DataMunicipio.FromAggregate(row, entidadFederativa));
    }

    public IEnumerable<DataMunicipio> Municipios { get; }
}