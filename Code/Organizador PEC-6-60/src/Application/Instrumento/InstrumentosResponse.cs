using System.Collections.Generic;

namespace Organizador_PEC_6_60.Application.Instrumento
{
    public class InstrumentosResponse
    {
        public IEnumerable<InstrumentoResponse> Instrumentos { get; }
        
        public InstrumentosResponse(IEnumerable<InstrumentoResponse> instrumentos)
        {
            Instrumentos = instrumentos;
        }
    }
}

