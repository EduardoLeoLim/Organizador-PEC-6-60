using Organizador_PEC_6_60.Application.EntidadFederativa.Search;

namespace Organizador_PEC_6_60.Application.Municipio.Search;

public class SearchMunicipiosByEntidadFederativa
{
    private readonly AllMunicipioSeacherService _allMunicipioSeacher;
    private readonly EntidadFederativaByIdSearcherService _entidadFederativaByIdSearcher;

    public SearchMunicipiosByEntidadFederativa(AllMunicipioSeacherService allMunicipioSeacher,
        EntidadFederativaByIdSearcherService entidadFederativaByIdSearcher)
    {
        _allMunicipioSeacher = allMunicipioSeacher;
        _entidadFederativaByIdSearcher = entidadFederativaByIdSearcher;
    }

    public DataMunicipios SearchByEntidadFederativa(int idEntidadFederativa)
    {
        return new DataMunicipios(
            _allMunicipioSeacher.SearchAll(idEntidadFederativa),
            _entidadFederativaByIdSearcher.SearchById(idEntidadFederativa)
        );
    }
}