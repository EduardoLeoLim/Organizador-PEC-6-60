using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search
{
    public class EntidadFederativaByIdSearcher
    {
        private readonly EntidadFederativaRepository _repository;

        public EntidadFederativaByIdSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public Domain.Model.EntidadFederativa SearchEntidadFederativaById(int id)
        {
            return _repository.SeacrhById(id);
        }
    }
}