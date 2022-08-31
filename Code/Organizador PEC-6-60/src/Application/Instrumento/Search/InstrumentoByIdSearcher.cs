using Organizador_PEC_6_60.Domain.Instrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Search
{
    public class InstrumentoByIdSearcher
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoByIdSearcher(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento SearchPEC_6_60ById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}