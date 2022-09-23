using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class EntidadFederativaAllSearcher : EntidadFederativaAllSearcherService
    {
        private readonly EntidadFederativaRepository _repository;

        public EntidadFederativaAllSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> SearchAll()
        {
            return _repository.SearchAll();
        }
    }
}