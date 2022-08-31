using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Search
{
    public class InstrumentoByCriteriaSearcher
    {
        private readonly InstrumentoRepository _repository;
        private Dictionary<string, object> criteria;

        public InstrumentoByCriteriaSearcher(InstrumentoRepository repository)
        {
            _repository = repository;
            criteria = new Dictionary<string, object>();
        }

        public InstrumentoByCriteriaSearcher TipoEstadistica(int idTipoEstadistica)
        {
            criteria["@IdTipoEstadistica"] = idTipoEstadistica;
            return this;
        }

        public InstrumentoByCriteriaSearcher TipoInstrumento(int idTipoInstrumento)
        {
            criteria["@IdInstrumento"] = idTipoInstrumento;
            return this;
        }

        public InstrumentoByCriteriaSearcher AñoEstadistico(string añoEstadistico)
        {
            criteria["@AñoEstadistico"] = añoEstadistico;
            return this;
        }

        public InstrumentoByCriteriaSearcher MesEstadistico(int mesEstadistico)
        {
            criteria["@MesEstadistico"] = mesEstadistico;
            return this;
        }

        public InstrumentoByCriteriaSearcher EntidadFederativa(int idEntidadFederativa)
        {
            criteria["@IdEntidadFederativa"] = idEntidadFederativa;
            return this;
        }

        public InstrumentoByCriteriaSearcher Municipio(int idMunicipio)
        {
            criteria["@IdMunicipio"] = idMunicipio;
            return this;
        }

        public InstrumentoByCriteriaSearcher Consecutivo(int consecutivo)
        {
            criteria["@Consecutivo"] = consecutivo;
            return this;
        }

        public InstrumentoByCriteriaSearcher GuardadoSIRESO(bool enSIRESO)
        {
            criteria["@Guardado"] = enSIRESO ? 1 : 0;
            return this;
        }
        
        public IEnumerable<Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento> SearchPEC_6_0()
        {
            var result = _repository.SearchByCriteria(criteria);
            criteria.Clear();//Clear filters if a instance of this class is reused
            return result;
        }
    }
}