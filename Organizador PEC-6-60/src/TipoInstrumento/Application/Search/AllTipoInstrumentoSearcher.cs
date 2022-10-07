using System.Collections.Generic;
using Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;

namespace Organizador_PEC_6_60.TipoInstrumento.Application.Search;

public class AllTipoInstrumentoSearcher
{
    private readonly TipoInstrumentoRepository _repository;

    public AllTipoInstrumentoSearcher(TipoInstrumentoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Domain.Model.TipoInstrumento> SearchAllInstrumentos()
    {
        return _repository.SearchAll();
    }
}