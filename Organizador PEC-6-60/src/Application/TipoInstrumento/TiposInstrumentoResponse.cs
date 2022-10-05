using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.TipoInstrumento;

public class TiposInstrumentoResponse
{
    public TiposInstrumentoResponse(IEnumerable<Domain.TipoInstrumento.Model.TipoInstrumento> instrumentos)
    {
        TiposInstrumento = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
    }

    public IEnumerable<TipoInstrumentoResponse> TiposInstrumento { get; }
}