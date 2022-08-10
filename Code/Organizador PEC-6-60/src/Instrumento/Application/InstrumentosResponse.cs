using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Instrumento.Application
{
    public class InstrumentosResponse
    {
        public IEnumerable<InstrumentoResponse> Instrumentos { get; }

        public InstrumentosResponse(IEnumerable<Domain.Model.Instrumento> instrumentos)
        {
            Instrumentos = instrumentos.Select(row => InstrumentoResponse.FromAggregate(row));
        }
    }
}