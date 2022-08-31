using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.TipoEstadistica
{
    public class TiposEstadisticaResponse
    {
        public IEnumerable<TipoEstadisticaResponse> TiposEstadistica { get; }

        public TiposEstadisticaResponse(IEnumerable<Domain.TipoEstadistica.Model.TipoEstadistica> tiposEstadistica)
        {
            TiposEstadistica = tiposEstadistica.Select(item => TipoEstadisticaResponse.FromAggregate(item));
        }
    }
}