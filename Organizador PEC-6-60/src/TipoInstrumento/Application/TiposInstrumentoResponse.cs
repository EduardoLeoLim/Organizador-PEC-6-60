using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.TipoInstrumento.Application;

public class TiposInstrumentoResponse
{
    public TiposInstrumentoResponse(IEnumerable<Domain.Model.TipoInstrumento> instrumentos)
    {
        TiposInstrumento = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
    }

    public IEnumerable<TipoInstrumentoResponse> TiposInstrumento { get; }
}