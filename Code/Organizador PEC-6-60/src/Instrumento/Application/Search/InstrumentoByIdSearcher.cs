using Organizador_PEC_6_60.Instrumento.Domain.Repository;

namespace Organizador_PEC_6_60.Instrumento.Application.Search
{
    public class InstrumentoByIdSearcher
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoByIdSearcher(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public Domain.Model.Instrumento SearchInstrumentoById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}