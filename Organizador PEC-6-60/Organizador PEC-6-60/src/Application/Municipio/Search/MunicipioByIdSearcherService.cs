namespace Organizador_PEC_6_60.Application.Municipio.Search
{
    public interface MunicipioByIdSearcherService
    {
        public Domain.Municipio.Model.Municipio SearchMunicipioById(int idMunicipio);
    }
}