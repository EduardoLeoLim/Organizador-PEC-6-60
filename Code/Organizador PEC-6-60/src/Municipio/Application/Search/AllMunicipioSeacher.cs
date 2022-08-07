using System.Collections.Generic;
using Organizador_PEC_6_60.Municipio.Domain.Repository;

namespace Organizador_PEC_6_60.Municipio.Application.Search
{
    public class AllMunicipioSeacher
    {
        private readonly MunicipioRepository _repository;

        public AllMunicipioSeacher(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Municipio.Domain.Model.Municipio> SearchAllMunicipios(int idEntidadFederativa)
        {
            return _repository.SearchAll(idEntidadFederativa);
        }
    }
}