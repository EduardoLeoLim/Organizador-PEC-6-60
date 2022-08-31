using System.Linq;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Instrumento.Create;
using Organizador_PEC_6_60.Application.Instrumento.Delete;
using Organizador_PEC_6_60.Application.Instrumento.Search;
using Organizador_PEC_6_60.Application.Instrumento.Update;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Application.TipoEstadistica.Search;
using Organizador_PEC_6_60.Application.TipoInstrumento.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.PEC_6_60.Application;
using Organizador_PEC_6_60.PEC_6_60.Application.Search;

namespace Organizador_PEC_6_60.Application.Instrumento
{
    public class ManageInstrumento
    {
        private InstrumentoByCriteriaSearcher _PEC_6_60ByCriteriaSearcher;
        private InstrumentoByIdSearcher _PEC_6_60ByIdSearcher;
        private InstrumentoCreator _PEC_6_60Creator;
        private InstrumentoUpdater _PEC_6_60Updater;
        private InstrumentoSIRESO _PEC_6_60SiresoUpdater;
        private InstrumentoDeleter _PEC_6_60Deleter;
        private TipoEstadisticaByIdSearcher _tipoEstadisticaByIdSearcher;
        private TipoInstrumentoByIdSearcher _tipoInstrumentoByIdSearcher;
        private MunicipioByIdSearcher _municipioByIdSearcher;
        private EntidadFederativaByIdSearcher _entidadFederativaByIdSearcher;

        public ManageInstrumento(InstrumentoRepository PEC_6_60Repository, TipoInstrumentoRepository instrumentoRepository,
            TipoEstadisticaRepository tipoEstadisticaRepository,
            EntidadFederativaRepository entidadFederativaRepository, MunicipioRepository municipioRepository)
        {
            _PEC_6_60ByCriteriaSearcher = new InstrumentoByCriteriaSearcher(PEC_6_60Repository);
            _PEC_6_60ByIdSearcher = new InstrumentoByIdSearcher(PEC_6_60Repository);
            _PEC_6_60Creator = new InstrumentoCreator(PEC_6_60Repository);
            _PEC_6_60Updater = new InstrumentoUpdater(PEC_6_60Repository);
            _PEC_6_60SiresoUpdater = new InstrumentoSIRESO(PEC_6_60Repository);
            _PEC_6_60Deleter = new InstrumentoDeleter(PEC_6_60Repository);
            _tipoEstadisticaByIdSearcher = new TipoEstadisticaByIdSearcher(tipoEstadisticaRepository);
            _tipoInstrumentoByIdSearcher = new TipoInstrumentoByIdSearcher(instrumentoRepository);
            _entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
            _municipioByIdSearcher = new MunicipioByIdSearcher(municipioRepository);
        }

        public InstrumentosResponse SearchPEC_6_60ByCriteria(int idTipoEstadistica = 0, int idTipoInstrumento = 0,
            string añoEstadistico = "", int mesEstadistico = 0, int idEntidadFederativa = 0, int idMunicipio = 0,
            int consecutivo = 0, FilterSIRESO guardadoSIRESO = FilterSIRESO.TODOS)
        {
            var searcher = _PEC_6_60ByCriteriaSearcher;
            if (idTipoEstadistica > 0)
                searcher = searcher.TipoEstadistica(idTipoEstadistica);
            if (idTipoInstrumento > 0)
                searcher = searcher.TipoInstrumento(idTipoInstrumento);
            if (añoEstadistico.Length > 0)
                searcher = searcher.AñoEstadistico(añoEstadistico);
            if (Enumerable.Range(1, 12).Contains(mesEstadistico))
                searcher = searcher.MesEstadistico(mesEstadistico);
            if (idEntidadFederativa > 0)
                searcher = searcher.EntidadFederativa(idEntidadFederativa);
            if (idMunicipio > 0)
                searcher = searcher.Municipio(idMunicipio);
            if (consecutivo > 0)
                searcher = searcher.Consecutivo(consecutivo);
            switch (guardadoSIRESO)
            {
                case FilterSIRESO.SI:
                    searcher = searcher.GuardadoSIRESO(true);
                    break;
                case FilterSIRESO.NO:
                    searcher = searcher.GuardadoSIRESO(false);
                    break;
            }

            var pec660s = searcher.SearchPEC_6_0();
            return new InstrumentosResponse(pec660s.Select(item =>
            {
                var municipio = _municipioByIdSearcher.SearchMunicipioById(item.IdMunicipio);
                var entidadFederativa =
                    _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
                var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(item.IdTipoEstadistica);
                var instrumento = _tipoInstrumentoByIdSearcher.SearchInstrumentoById(item.IdInstrumento);

                return InstrumentoResponse.FromAggregate(item, tipoEstadistica, instrumento, entidadFederativa, municipio);
            }));
        }

        public InstrumentoResponse SearchPEC_6_60ById(int id)
        {
            var pec660 = _PEC_6_60ByIdSearcher.SearchPEC_6_60ById(id);
            var municipio = _municipioByIdSearcher.SearchMunicipioById(pec660.IdMunicipio);
            var entidadFederativa =
                _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
            var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(pec660.IdTipoEstadistica);
            var instrumento = _tipoInstrumentoByIdSearcher.SearchInstrumentoById(pec660.IdInstrumento);

            return InstrumentoResponse.FromAggregate(pec660, tipoEstadistica, instrumento, entidadFederativa, municipio);
        }

        public void RegisterPEC_6_60(int idTipoEstadistica, int idInstrumento, int idMunicipio, string añoEstadistico,
            int mesEstadistico, int consecutivo, byte[] dataArchivo)
        {
            _PEC_6_60Creator.Create(new InstrumentoAñoEstadistico(añoEstadistico),
                new InstrumentoMesEstadistico(mesEstadistico), new InstrumentoConsecutivo(consecutivo), dataArchivo,
                idInstrumento, idTipoEstadistica, idMunicipio);
        }

        public void UpdatePEC_6_60(int id, string añoEstadistico, int mesEstadistico, int consecutivo,
            byte[] dataArchivo,
            int idInstrumento, int idTipoEstadistica, int idMunicipio)
        {
            _PEC_6_60Updater.Update(id, new InstrumentoAñoEstadistico(añoEstadistico),
                new InstrumentoMesEstadistico(mesEstadistico), new InstrumentoConsecutivo(consecutivo), dataArchivo,
                idInstrumento, idTipoEstadistica, idMunicipio);
        }

        public void PEC_6_60SavedInSIRESO(int id)
        {
            _PEC_6_60SiresoUpdater.SavedInSIRESO(id);
        }

        public void PEC_6_60UnsavedInSIRESO(int id)
        {
            _PEC_6_60SiresoUpdater.UnsavedInSIRESO(id);
        }

        public void DeletePEC_6_60(int id)
        {
            _PEC_6_60Deleter.Delete(id);
        }
    }
}