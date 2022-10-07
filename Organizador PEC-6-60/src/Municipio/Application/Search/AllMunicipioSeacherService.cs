using System.Collections.Generic;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public interface AllMunicipioSeacherService
{
    public IEnumerable<Domain.Model.Municipio> SearchAll(int idEntidadFederativa);
}