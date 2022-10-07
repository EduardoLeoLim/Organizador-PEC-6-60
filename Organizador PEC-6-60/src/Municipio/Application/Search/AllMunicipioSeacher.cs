using System.Collections.Generic;
using Organizador_PEC_6_60.Municipio.Domain.Repository;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public class AllMunicipioSeacher : AllMunicipioSeacherService
{
    private readonly MunicipioRepository _repository;

    public AllMunicipioSeacher(MunicipioRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Domain.Model.Municipio> SearchAll(int idEntidadFederativa)
    {
        return _repository.SearchAll(idEntidadFederativa);
    }
}