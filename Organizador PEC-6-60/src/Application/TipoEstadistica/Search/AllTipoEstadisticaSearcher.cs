using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;

namespace Organizador_PEC_6_60.Application.TipoEstadistica.Search;

public class AllTipoEstadisticaSearcher
{
    private readonly TipoEstadisticaRepository _repository;

    public AllTipoEstadisticaSearcher(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Domain.TipoEstadistica.Model.TipoEstadistica> SearchAllTiposEstadistica()
    {
        return _repository.SearchAll();
    }
}