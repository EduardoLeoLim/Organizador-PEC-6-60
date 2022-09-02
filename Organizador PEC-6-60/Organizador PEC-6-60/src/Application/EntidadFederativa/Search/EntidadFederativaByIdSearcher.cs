using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class EntidadFederativaByIdSearcher
    {
        private readonly EntidadFederativaRepository _repository;

        public EntidadFederativaByIdSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public Domain.EntidadFederativa.Model.EntidadFederativa SearchEntidadFederativaById(int id)
        {
            return _repository.SeacrhById(id);
        }
    }
}