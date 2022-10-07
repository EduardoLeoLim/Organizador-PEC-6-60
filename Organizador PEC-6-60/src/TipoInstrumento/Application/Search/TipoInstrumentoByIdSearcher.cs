using Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;

namespace Organizador_PEC_6_60.TipoInstrumento.Application.Search;

public class TipoInstrumentoByIdSearcher
{
    private readonly TipoInstrumentoRepository _repository;

    public TipoInstrumentoByIdSearcher(TipoInstrumentoRepository repository)
    {
        _repository = repository;
    }

    public Domain.Model.TipoInstrumento SearchTipoInstrumentoById(int id)
    {
        return _repository.SearchById(id);
    }
}