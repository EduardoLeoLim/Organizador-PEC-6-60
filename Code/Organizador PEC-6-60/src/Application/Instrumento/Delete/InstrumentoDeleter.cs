using Organizador_PEC_6_60.Domain.Instrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Delete
{
    public class InstrumentoDeleter
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoDeleter(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}