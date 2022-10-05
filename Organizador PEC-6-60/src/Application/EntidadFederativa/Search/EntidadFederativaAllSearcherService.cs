using System.Collections.Generic;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search;

public interface EntidadFederativaAllSearcherService
{
    public IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> SearchAll();
}