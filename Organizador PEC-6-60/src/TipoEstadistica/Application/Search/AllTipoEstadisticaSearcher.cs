using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class AllTipoEstadisticaSearcher : AllTipoEstadisticaSearcherService
{
    private readonly TipoEstadisticaRepository _repository;

    public AllTipoEstadisticaSearcher(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Domain.Model.TipoEstadistica> SearchAll()
    {
        return _repository.SearchAll();
    }
}