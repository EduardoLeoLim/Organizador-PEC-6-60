using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;

namespace Organizador_PEC_6_60.Application.TipoInstrumento.Search
{
    public class AllTipoInstrumentoSearcher
    {
        private readonly TipoInstrumentoRepository _repository;

        public AllTipoInstrumentoSearcher(TipoInstrumentoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.TipoInstrumento.Model.TipoInstrumento> SearchAllInstrumentos()
        {
            return _repository.SearchAll();
        }
    }
}