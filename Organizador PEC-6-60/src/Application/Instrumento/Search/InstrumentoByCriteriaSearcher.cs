using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Search;

public class InstrumentoByCriteriaSearcher
{
    private readonly InstrumentoRepository _repository;
    private readonly Dictionary<string, object> _criteria;

    public InstrumentoByCriteriaSearcher(InstrumentoRepository repository)
    {
        _repository = repository;
        _criteria = new Dictionary<string, object>();
    }

    public InstrumentoByCriteriaSearcher TipoEstadistica(int idTipoEstadistica)
    {
        _criteria["@IdTipoEstadistica"] = idTipoEstadistica;
        return this;
    }

    public InstrumentoByCriteriaSearcher TipoInstrumento(int idTipoInstrumento)
    {
        _criteria["@IdInstrumento"] = idTipoInstrumento;
        return this;
    }

    public InstrumentoByCriteriaSearcher AñoEstadistico(string añoEstadistico)
    {
        _criteria["@AñoEstadistico"] = añoEstadistico;
        return this;
    }

    public InstrumentoByCriteriaSearcher MesEstadistico(int mesEstadistico)
    {
        _criteria["@MesEstadistico"] = mesEstadistico;
        return this;
    }

    public InstrumentoByCriteriaSearcher EntidadFederativa(int idEntidadFederativa)
    {
        _criteria["@IdEntidadFederativa"] = idEntidadFederativa;
        return this;
    }

    public InstrumentoByCriteriaSearcher Municipio(int idMunicipio)
    {
        _criteria["@IdMunicipio"] = idMunicipio;
        return this;
    }

    public InstrumentoByCriteriaSearcher Consecutivo(int consecutivo)
    {
        _criteria["@Consecutivo"] = consecutivo;
        return this;
    }

    public InstrumentoByCriteriaSearcher GuardadoSIRESO(bool enSIRESO)
    {
        _criteria["@Guardado"] = enSIRESO ? 1 : 0;
        return this;
    }

    public IEnumerable<Domain.Instrumento.Model.Instrumento> SearchInstrumentos()
    {
        var result = _repository.SearchByCriteria(_criteria);
        _criteria.Clear(); //Clear filters if a instance of this class is reused
        return result;
    }
}