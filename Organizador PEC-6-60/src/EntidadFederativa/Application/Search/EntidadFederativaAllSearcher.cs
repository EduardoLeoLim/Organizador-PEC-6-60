using System.Collections.Generic;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search;

public class EntidadFederativaAllSearcher : EntidadFederativaAllSearcherService
{
    private readonly EntidadFederativaRepository _repository;

    public EntidadFederativaAllSearcher(EntidadFederativaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Domain.Model.EntidadFederativa> SearchAll()
    {
        return _repository.SearchAll();
    }
}