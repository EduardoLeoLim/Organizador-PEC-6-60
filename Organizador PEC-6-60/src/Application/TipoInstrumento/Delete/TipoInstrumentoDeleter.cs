using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;

namespace Organizador_PEC_6_60.Application.TipoInstrumento.Delete
{
    public class TipoInstrumentoDeleter
    {
        private readonly TipoInstrumentoRepository _repository;

        public TipoInstrumentoDeleter(TipoInstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}