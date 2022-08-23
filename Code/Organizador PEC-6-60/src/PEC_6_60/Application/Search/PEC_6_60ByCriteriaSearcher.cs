using System.Collections.Generic;
using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Search
{
    public class PEC_6_60ByCriteriaSearcher
    {
        private readonly PEC_6_60Repository _repository;
        private Dictionary<string, object> criteria;

        public PEC_6_60ByCriteriaSearcher(PEC_6_60Repository repository)
        {
            _repository = repository;
            criteria = new Dictionary<string, object>();
        }

        public PEC_6_60ByCriteriaSearcher TipoEstadistica(int idTipoEstadistica)
        {
            criteria["@IdTipoEstadistica"] = idTipoEstadistica;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher Instrumento(int idInstrumento)
        {
            criteria["@IdInstrumento"] = idInstrumento;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher AñoEstadistico(string añoEstadistico)
        {
            criteria["@AñoEstadistico"] = añoEstadistico;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher MesEstadistico(int mesEstadistico)
        {
            criteria["@MesEstadistico"] = mesEstadistico;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher EntidadFederativa(int idEntidadFederativa)
        {
            criteria["@IdEntidadFederativa"] = idEntidadFederativa;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher Municipio(int idMunicipio)
        {
            criteria["@IdMunicipio"] = idMunicipio;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher Consecutivo(int consecutivo)
        {
            criteria["@Consecutivo"] = consecutivo;
            return this;
        }

        public PEC_6_60ByCriteriaSearcher GuardadoSIRESO(bool enSIRESO)
        {
            criteria["@Guardado"] = enSIRESO ? 1 : 0;
            return this;
        }
        
        public IEnumerable<Domain.Model.PEC_6_60> SearchPEC_6_0()
        {
            var result = _repository.SearchByCriteria(criteria);
            criteria.Clear();//Clear filters if a instance of this class is reused
            return result;
        }
    }
}