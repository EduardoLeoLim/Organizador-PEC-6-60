using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Model;

namespace Organizador_PEC_6_60.Instrumento.Application
{
    public class TiposInstrumentoResponse
    {
        public IEnumerable<TipoInstrumentoResponse> TiposInstrumento { get; }

        public TiposInstrumentoResponse(IEnumerable<TipoInstrumento> instrumentos)
        {
            TiposInstrumento = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
        }
    }
}