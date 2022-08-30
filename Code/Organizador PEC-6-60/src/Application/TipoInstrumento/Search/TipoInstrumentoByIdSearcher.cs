using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;

namespace Organizador_PEC_6_60.Application.TipoInstrumento.Search
{
    public class TipoInstrumentoByIdSearcher
    {
        private readonly TipoInstrumentoRepository _repository;

        public TipoInstrumentoByIdSearcher(TipoInstrumentoRepository repository)
        {
            _repository = repository;
        }

        public Domain.TipoInstrumento.Model.TipoInstrumento SearchInstrumentoById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}