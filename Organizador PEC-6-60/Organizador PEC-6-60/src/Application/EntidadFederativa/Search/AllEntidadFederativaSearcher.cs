using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class AllEntidadFederativaSearcher
    {
        private readonly EntidadFederativaRepository _repository;

        public AllEntidadFederativaSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> SearchAllEntidadesFederativas()
        {
            return _repository.SearchAll();
        }
    }
}