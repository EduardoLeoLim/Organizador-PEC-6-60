using Organizador_PEC_6_60.EntidadFederativa.Application.Search;

namespace Organizador_PEC_6_60.Municipio.Application.Search;

public class SearchMunicipioById
{
    private readonly EntidadFederativaByIdSearcherService _entidadFederativaByIdSearcher;
    private readonly MunicipioByIdSearcherService _municipioByIdSearcher;

    public SearchMunicipioById(MunicipioByIdSearcherService municipioByIdSearcher,
        EntidadFederativaByIdSearcher entidadFederativaByIdSearcher)
    {
        _municipioByIdSearcher = municipioByIdSearcher;
        _entidadFederativaByIdSearcher = entidadFederativaByIdSearcher;
    }

    public DataMunicipio SearchById(int idMunicipio)
    {
        var municipio = _municipioByIdSearcher.SearchMunicipioById(idMunicipio);
        var entidadFederativa = _entidadFederativaByIdSearcher.SearchById(municipio.IdEntidadFederativa);

        return DataMunicipio.FromAggregate(municipio, entidadFederativa);
    }
}