using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class TipoEstadisticaByIdSearcher : TipoEstadisticaByIdSearcherService
{
    private readonly TipoEstadisticaRepository _repository;

    public TipoEstadisticaByIdSearcher(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public Domain.Model.TipoEstadistica SearchTipoEstadisticaById(int id)
    {
        return _repository.SearchById(id);
    }
}