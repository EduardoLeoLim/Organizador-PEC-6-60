using System.Collections.Generic;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search
{
    public class AllEntidadFederativaSearcher
    {
        private readonly EntidadFederativaRepository _repository;

        public AllEntidadFederativaSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.Model.EntidadFederativa> SearchAllEntidadesFederativas()
        {
            return _repository.SearchAll();
        }
    }
}