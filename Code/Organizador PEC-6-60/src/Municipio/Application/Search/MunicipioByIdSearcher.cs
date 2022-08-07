using Organizador_PEC_6_60.Municipio.Domain.Repository;

namespace Organizador_PEC_6_60.Municipio.Application.Search
{
    public class MunicipioByIdSearcher
    {
        private readonly MunicipioRepository _repository;

        public MunicipioByIdSearcher(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public Municipio.Domain.Model.Municipio SearchMunicipioById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}