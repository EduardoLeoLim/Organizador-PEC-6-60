using Organizador_PEC_6_60.Domain.Municipio.Repository;

namespace Organizador_PEC_6_60.Application.Municipio.Search
{
    public class MunicipioByIdSearcher
    {
        private readonly MunicipioRepository _repository;

        public MunicipioByIdSearcher(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public Domain.Municipio.Model.Municipio SearchMunicipioById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}