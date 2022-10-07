using System.Collections.Generic;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search;

public interface EntidadFederativaAllSearcherService
{
    public IEnumerable<Domain.Model.EntidadFederativa> SearchAll();
}