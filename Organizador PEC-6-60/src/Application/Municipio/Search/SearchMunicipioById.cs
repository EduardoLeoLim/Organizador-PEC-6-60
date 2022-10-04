using Organizador_PEC_6_60.Application.EntidadFederativa.Search;

namespace Organizador_PEC_6_60.Application.Municipio.Search
{
    public class SearchMunicipioById
    {
        private readonly MunicipioByIdSearcherService _municipioByIdSearcher;
        private readonly EntidadFederativaByIdSearcherService _entidadFederativaByIdSearcher;

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
}