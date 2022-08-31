using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.EntidadFederativa
{
    public class EntidadesFederativasResponse
    {
        public IEnumerable<EntidadFederativaResponse> EntidadesFederativas { get; }

        public EntidadesFederativasResponse(
            IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> entidadesFederativas)
        {
            EntidadesFederativas = entidadesFederativas.Select(row => EntidadFederativaResponse.FromAggregate(row));
        }
    }
}