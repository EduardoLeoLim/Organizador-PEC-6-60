using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class EntidadFederativaByIdSearcher : EntidadFederativaByIdSearcherService
    {
        private readonly EntidadFederativaRepository _repository;

        public EntidadFederativaByIdSearcher(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public Domain.EntidadFederativa.Model.EntidadFederativa SearchById(int idEntidadFederetiva)
        {
            return _repository.SeacrhById(idEntidadFederetiva);
        }
    }
}