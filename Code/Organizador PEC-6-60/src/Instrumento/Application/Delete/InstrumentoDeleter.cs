using Organizador_PEC_6_60.Instrumento.Domain.Repository;

namespace Organizador_PEC_6_60.Instrumento.Application.Delete
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