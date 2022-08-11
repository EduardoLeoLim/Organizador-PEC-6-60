using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.TipoEstadistica.Application;

public class TiposEstadisticaResponse
{
    public IEnumerable<TipoEstadisticaResponse> TiposEstadistica { get; }

    public TiposEstadisticaResponse(IEnumerable<Domain.Model.TipoEstadistica> tiposEstadistica)
    {
        TiposEstadistica = tiposEstadistica.Select(row => TipoEstadisticaResponse.FromAggregate(row));
    }
}