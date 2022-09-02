using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Application.TipoEstadistica.Search;
using Organizador_PEC_6_60.Application.TipoInstrumento.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Search
{
    public class SearchInstrumentoById
    {
        private readonly InstrumentoByIdSearcher _instrumentoByIdSearcher;
        private readonly MunicipioByIdSearcher _municipioByIdSearcher;
        private readonly EntidadFederativaByIdSearcher _entidadFederativaByIdSearcher;
        private readonly TipoEstadisticaByIdSearcher _tipoEstadisticaByIdSearcher;
        private readonly TipoInstrumentoByIdSearcher _tipoInstrumentoByIdSearcher;

        public SearchInstrumentoById(
            InstrumentoRepository instrumentoRepository,
            MunicipioRepository municipioRepository,
            EntidadFederativaRepository entidadFederativaRepository,
            TipoEstadisticaRepository tipoEstadisticaRepository,
            TipoInstrumentoRepository tipoInstrumentoRepository
        )
        {
            _instrumentoByIdSearcher = new InstrumentoByIdSearcher(instrumentoRepository);
            _municipioByIdSearcher = new MunicipioByIdSearcher(municipioRepository);
            _entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
            _tipoEstadisticaByIdSearcher = new TipoEstadisticaByIdSearcher(tipoEstadisticaRepository);
            _tipoInstrumentoByIdSearcher = new TipoInstrumentoByIdSearcher(tipoInstrumentoRepository);
        }

        public InstrumentoData SearchInstrumento(int id)
        {
            var instrumento = _instrumentoByIdSearcher.SearchInstrumentoById(id);
            var municipio = _municipioByIdSearcher.SearchMunicipioById(instrumento.IdMunicipio);
            var entidadFederativa =
                _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
            var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(instrumento.IdTipoEstadistica);
            var tipoInstrumento = _tipoInstrumentoByIdSearcher.SearchTipoInstrumentoById(instrumento.IdInstrumento);

            return InstrumentoData.FromAggregate(
                instrumento,
                tipoEstadistica,
                tipoInstrumento,
                entidadFederativa,
                municipio
            );
        }
    }
}