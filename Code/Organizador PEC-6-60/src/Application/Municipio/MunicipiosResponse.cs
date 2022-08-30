using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.Municipio
{
    public class MunicipiosResponse
    {
        public IEnumerable<MunicipioResponse> Municipios { get; }

        public MunicipiosResponse(IEnumerable<Domain.Municipio.Model.Municipio> municipios, Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            Municipios = municipios.Select(row => MunicipioResponse.FromAggregate(row, entidadFederativa));
        }
    }
}