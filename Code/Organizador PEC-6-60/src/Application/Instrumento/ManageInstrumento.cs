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

namespace Organizador_PEC_6_60.Application.Instrumento
{
    public class ManageInstrumento
    {
        private InstrumentoByCriteriaSearcher _instrumentoByCriteriaSearcher;
        private InstrumentoByIdSearcher _instrumentoByIdSearcher;
        private InstrumentoCreator _instrumentoCreator;
        private InstrumentoUpdater _instrumentoUpdater;
        private InstrumentoSIRESO _instrumentoSiresoUpdater;
        private InstrumentoDeleter _instrumentoDeleter;
        private TipoEstadisticaByIdSearcher _tipoEstadisticaByIdSearcher;
        private TipoInstrumentoByIdSearcher _tipoInstrumentoByIdSearcher;
        private MunicipioByIdSearcher _municipioByIdSearcher;
        private EntidadFederativaByIdSearcher _entidadFederativaByIdSearcher;

        public ManageInstrumento(
            InstrumentoRepository instrumentoRepository,
            TipoInstrumentoRepository tipoInstrumentoRepository,
            TipoEstadisticaRepository tipoEstadisticaRepository,
            EntidadFederativaRepository entidadFederativaRepository,
            MunicipioRepository municipioRepository
        )
        {
            _instrumentoByCriteriaSearcher = new InstrumentoByCriteriaSearcher(instrumentoRepository);
            _instrumentoByIdSearcher = new InstrumentoByIdSearcher(instrumentoRepository);
            _instrumentoCreator = new InstrumentoCreator(instrumentoRepository);
            _instrumentoUpdater = new InstrumentoUpdater(instrumentoRepository);
            _instrumentoSiresoUpdater = new InstrumentoSIRESO(instrumentoRepository);
            _instrumentoDeleter = new InstrumentoDeleter(instrumentoRepository);
            _tipoEstadisticaByIdSearcher = new TipoEstadisticaByIdSearcher(tipoEstadisticaRepository);
            _tipoInstrumentoByIdSearcher = new TipoInstrumentoByIdSearcher(tipoInstrumentoRepository);
            _entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
            _municipioByIdSearcher = new MunicipioByIdSearcher(municipioRepository);
        }

        public InstrumentosResponse SearchInstrumentoByCriteria(
            int idTipoEstadistica = 0,
            int idTipoInstrumento = 0,
            string añoEstadistico = "",
            int mesEstadistico = 0,
            int idEntidadFederativa = 0,
            int idMunicipio = 0,
            int consecutivo = 0,
            FilterSIRESO guardadoSIRESO = FilterSIRESO.TODOS
        )
        {
            var searcher = _instrumentoByCriteriaSearcher;
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

            var instrumentos = searcher.SearchInstrumentos();

            return new InstrumentosResponse(
                instrumentos.Select(
                    item =>
                    {
                        var municipio = _municipioByIdSearcher.SearchMunicipioById(item.IdMunicipio);
                        var entidadFederativa =
                            _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
                        var tipoEstadistica =
                            _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(item.IdTipoEstadistica);
                        var instrumento = _tipoInstrumentoByIdSearcher.SearchTipoInstrumentoById(item.IdInstrumento);

                        return InstrumentoResponse.FromAggregate(
                            item,
                            tipoEstadistica,
                            instrumento,
                            entidadFederativa,
                            municipio
                        );
                    }
                )
            );
        }

        public InstrumentoResponse SearchInstrumentoById(int id)
        {
            var instrumento = _instrumentoByIdSearcher.SearchInstrumentoById(id);
            var municipio = _municipioByIdSearcher.SearchMunicipioById(instrumento.IdMunicipio);
            var entidadFederativa =
                _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
            var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(instrumento.IdTipoEstadistica);
            var tipoInstrumento = _tipoInstrumentoByIdSearcher.SearchTipoInstrumentoById(instrumento.IdInstrumento);

            return InstrumentoResponse.FromAggregate(
                instrumento,
                tipoEstadistica,
                tipoInstrumento,
                entidadFederativa,
                municipio
            );
        }

        public void RegisterPEC_6_60(
            int idTipoEstadistica,
            int idInstrumento,
            int idMunicipio,
            string añoEstadistico,
            int mesEstadistico,
            int consecutivo,
            byte[] dataArchivo
        )
        {
            _instrumentoCreator.Create(
                new InstrumentoAñoEstadistico(añoEstadistico),
                new InstrumentoMesEstadistico(mesEstadistico),
                new InstrumentoConsecutivo(consecutivo),
                dataArchivo,
                idInstrumento,
                idTipoEstadistica,
                idMunicipio
            );
        }

        public void UpdateInstrumento(
            int id,
            string añoEstadistico,
            int mesEstadistico,
            int consecutivo,
            byte[] dataArchivo,
            int idInstrumento,
            int idTipoEstadistica,
            int idMunicipio
        )
        {
            _instrumentoUpdater.Update(
                id,
                new InstrumentoAñoEstadistico(añoEstadistico),
                new InstrumentoMesEstadistico(mesEstadistico),
                new InstrumentoConsecutivo(consecutivo),
                dataArchivo,
                idInstrumento,
                idTipoEstadistica,
                idMunicipio
            );
        }

        public void InstrumentoSavedInSIRESO(int id)
        {
            _instrumentoSiresoUpdater.SavedInSIRESO(id);
        }

        public void InstrumentoUnsavedInSIRESO(int id)
        {
            _instrumentoSiresoUpdater.UnsavedInSIRESO(id);
        }

        public void DeleteInstrumento(int id)
        {
            _instrumentoDeleter.Delete(id);
        }
    }
}