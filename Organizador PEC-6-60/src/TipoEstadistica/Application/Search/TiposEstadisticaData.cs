using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class TiposEstadisticaData
{
    public TiposEstadisticaData(IEnumerable<Domain.Model.TipoEstadistica> tiposEstadistica)
    {
        TiposEstadistica = tiposEstadistica.Select(item => TipoEstadisticaData.FromAggregate(item));
    }

    public IEnumerable<TipoEstadisticaData> TiposEstadistica { get; }
}