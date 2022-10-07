using Organizador_PEC_6_60.EntidadFederativa.Application.Search;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

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