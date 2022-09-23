using System.Collections.Generic;
using System.Linq;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class DataEntidadesFederativas
    {
        public IEnumerable<DataEntidadFederativa> EntidadesFederativas { get; }

        public DataEntidadesFederativas(
            IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> entidadesFederativas)
        {
            EntidadesFederativas = entidadesFederativas.Select(row => DataEntidadFederativa.FromAggregate(row));
        }
    }
}