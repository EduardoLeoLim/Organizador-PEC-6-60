using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Municipio.Application
{
    public class MunicipiosResponse
    {
        public IEnumerable<MunicipioResponse> Municipios { get; }

        public MunicipiosResponse(IEnumerable<Domain.Model.Municipio> municipios)
        {
            Municipios = municipios.Select(row => MunicipioResponse.FromAggregate(row));
        }
    }
}