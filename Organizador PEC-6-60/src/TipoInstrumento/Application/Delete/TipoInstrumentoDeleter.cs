using Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;

namespace Organizador_PEC_6_60.TipoInstrumento.Application.Delete;

public class TipoInstrumentoDeleter
{
    private readonly TipoInstrumentoRepository _repository;

    public TipoInstrumentoDeleter(TipoInstrumentoRepository repository)
    {
        _repository = repository;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}