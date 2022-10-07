using System.Linq;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Municipio.Application.Search;
using Organizador_PEC_6_60.Municipio.Domain.Repository;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;
using Organizador_PEC_6_60.TipoInstrumento.Application.Search;
using Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;

namespace Organizador_PEC_6_60.Instrumento.Application.Search;

public class SearchInstrumentoByCriteria
{
    private readonly EntidadFederativaByIdSearcher _entidadFederativaByIdSearcher;
    private readonly MunicipioByIdSearcher _municipioByIdSearcher;
    private readonly TipoEstadisticaByIdSearcher _tipoEstadisticaByIdSearcher;
    private readonly TipoInstrumentoByIdSearcher _tipoInstrumentoByIdSearcher;
    private InstrumentoByCriteriaSearcher _instrumentoByCriteriaSearcher;

    public SearchInstrumentoByCriteria(
        InstrumentoRepository instrumentoRepository,
        MunicipioRepository municipioRepository,
        EntidadFederativaRepository entidadFederativaRepository,
        TipoEstadisticaRepository tipoEstadisticaRepository,
        TipoInstrumentoRepository tipoInstrumentoRepository
    )
    {
        _instrumentoByCriteriaSearcher = new InstrumentoByCriteriaSearcher(instrumentoRepository);
        _municipioByIdSearcher = new MunicipioByIdSearcher(municipioRepository);
        _entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
        _tipoEstadisticaByIdSearcher = new TipoEstadisticaByIdSearcher(tipoEstadisticaRepository);
        _tipoInstrumentoByIdSearcher = new TipoInstrumentoByIdSearcher(tipoInstrumentoRepository);
    }

    public SearchInstrumentoByCriteria TipoEstadistica(int idTipoEstadistica)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.TipoEstadistica(idTipoEstadistica);
        return this;
    }

    public SearchInstrumentoByCriteria TipoInstrumento(int idTipoInstrumento)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.TipoInstrumento(idTipoInstrumento);
        return this;
    }

    public SearchInstrumentoByCriteria AñoEstadistico(string añoEstadistico)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.AñoEstadistico(añoEstadistico);
        return this;
    }

    public SearchInstrumentoByCriteria MesEstadistico(int mesEstadistico)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.MesEstadistico(mesEstadistico);
        return this;
    }

    public SearchInstrumentoByCriteria EntidadFederativa(int idEntidadFederativa)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.EntidadFederativa(idEntidadFederativa);
        return this;
    }

    public SearchInstrumentoByCriteria Municipio(int idMunicipio)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.Municipio(idMunicipio);
        return this;
    }

    public SearchInstrumentoByCriteria Consecutivo(int consecutivo)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.Consecutivo(consecutivo);
        return this;
    }

    public SearchInstrumentoByCriteria GuardadoSIRESO(bool enSIRESO)
    {
        _instrumentoByCriteriaSearcher = _instrumentoByCriteriaSearcher.GuardadoSIRESO(enSIRESO);
        return this;
    }

    public InstrumentosData SearchInstrumentos()
    {
        var instrumentos = _instrumentoByCriteriaSearcher.SearchInstrumentos();
        var instrumentosData = instrumentos.Select(
            instrumento =>
            {
                var municipio = _municipioByIdSearcher.SearchMunicipioById(instrumento.IdMunicipio);
                var entidadFederativa =
                    _entidadFederativaByIdSearcher.SearchById(municipio.IdEntidadFederativa);
                var tipoEstadistica =
                    _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(instrumento.IdTipoEstadistica);
                var tipoInstrumento =
                    _tipoInstrumentoByIdSearcher.SearchTipoInstrumentoById(instrumento.IdInstrumento);

                return InstrumentoData.FromAggregate(
                    instrumento,
                    tipoEstadistica,
                    tipoInstrumento,
                    entidadFederativa,
                    municipio
                );
            }
        );
        return new InstrumentosData(instrumentosData);
    }
}