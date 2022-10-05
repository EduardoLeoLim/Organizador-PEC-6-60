using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;

namespace Organizador_PEC_6_60.Application.TipoEstadistica.Delete;

public class TipoEstadisticaDeleter
{
    private readonly TipoEstadisticaRepository _repository;

    public TipoEstadisticaDeleter(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}