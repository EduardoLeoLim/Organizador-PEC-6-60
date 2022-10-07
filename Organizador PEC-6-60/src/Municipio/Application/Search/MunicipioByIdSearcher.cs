using Organizador_PEC_6_60.Municipio.Domain.Repository;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public class MunicipioByIdSearcher : MunicipioByIdSearcherService
{
    private readonly MunicipioRepository _repository;

    public MunicipioByIdSearcher(MunicipioRepository repository)
    {
        _repository = repository;
    }

    public Domain.Model.Municipio SearchMunicipioById(int idMunicipio)
    {
        return _repository.SearchById(idMunicipio);
    }
}