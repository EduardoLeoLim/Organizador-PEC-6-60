using Organizador_PEC_6_60.Domain.Municipio.Repository;

namespace Organizador_PEC_6_60.Application.Municipio.Delete
{
    public class MunicipioDeleter
    {
        private readonly MunicipioRepository _repository;

        public MunicipioDeleter(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}