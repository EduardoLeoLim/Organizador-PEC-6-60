using System.Collections.Generic;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public interface AllTipoEstadisticaSearcherService
{
    IEnumerable<Domain.Model.TipoEstadistica> SearchAll();
}