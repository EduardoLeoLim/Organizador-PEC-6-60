using System.Collections.Generic;

namespace Organizador_PEC_6_60.Application.Municipio.Search;

public interface AllMunicipioSeacherService
{
    public IEnumerable<Domain.Municipio.Model.Municipio> SearchAll(int idEntidadFederativa);
}