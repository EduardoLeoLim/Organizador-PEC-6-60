using System.Collections.Generic;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;

namespace Organizador_PEC_6_60.Instrumento.Application.Search
{
    public class AllInstrumentoSearcher
    {
        private readonly InstrumentoRepository _repository;

        public AllInstrumentoSearcher(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.Model.Instrumento> SearchAllInstrumentos()
        {
            return _repository.SearchAll();
        }
    }
}