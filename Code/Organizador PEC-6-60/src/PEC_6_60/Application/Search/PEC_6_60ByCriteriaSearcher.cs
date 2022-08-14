using System.Collections.Generic;
using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Search
{
    public class PEC_6_60ByCriteriaSearcher
    {
        private readonly PEC_6_60Repository _repository;

        public PEC_6_60ByCriteriaSearcher(PEC_6_60Repository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.Model.PEC_6_60> SearchPEC_6_0ByCriteria(int idTipoEstadistica, int idInstrumento,
            string añoEstadistico, int mesEstadistico, int idEntidadFederativa, int idMunicipio, int consecutivo)
        {
            var criteria = new Dictionary<string, object>();
            if (idTipoEstadistica > 0)
                criteria.Add("@IdTipoEstadistica", idTipoEstadistica);
            if (idInstrumento > 0)
                criteria.Add("@IdInstrumento", idInstrumento);
            if (añoEstadistico.Length > 0)
                criteria.Add("@AñoEstadistico", añoEstadistico);
            if (mesEstadistico > 0)
                criteria.Add("@MesEstadistico", mesEstadistico);
            if (idEntidadFederativa > 0)
                criteria.Add("@IdEntidadFederativa", idEntidadFederativa);
            if (idMunicipio > 0)
                criteria.Add("@IdMunicipio", idMunicipio);
            if (consecutivo > 0)
                criteria.Add("@Consecutivo", consecutivo);

            return _repository.SearchByCriteria(criteria);
        }
    }
}