﻿using System.Linq;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Application.TipoInstrumento.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.PEC_6_60.Application.Create;
using Organizador_PEC_6_60.PEC_6_60.Application.Delete;
using Organizador_PEC_6_60.PEC_6_60.Application.Search;
using Organizador_PEC_6_60.PEC_6_60.Application.Update;
using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;
using Organizador_PEC_6_60.PEC_6_60.Domain.ValueObjects;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application
{
    public class ManagePEC_6_60
    {
        private PEC_6_60ByCriteriaSearcher _PEC_6_60ByCriteriaSearcher;
        private PEC_6_60ByIdSearcher _PEC_6_60ByIdSearcher;
        private PEC_6_60Creator _PEC_6_60Creator;
        private PEC_6_60Updater _PEC_6_60Updater;
        private PEC_6_60SIRESO _PEC_6_60SiresoUpdater;
        private PEC_6_60Deleter _PEC_6_60Deleter;
        private TipoEstadisticaByIdSearcher _tipoEstadisticaByIdSearcher;
        private TipoInstrumentoByIdSearcher _tipoInstrumentoByIdSearcher;
        private MunicipioByIdSearcher _municipioByIdSearcher;
        private EntidadFederativaByIdSearcher _entidadFederativaByIdSearcher;

        public ManagePEC_6_60(PEC_6_60Repository PEC_6_60Repository, TipoInstrumentoRepository instrumentoRepository,
            TipoEstadisticaRepository tipoEstadisticaRepository,
            EntidadFederativaRepository entidadFederativaRepository, MunicipioRepository municipioRepository)
        {
            _PEC_6_60ByCriteriaSearcher = new PEC_6_60ByCriteriaSearcher(PEC_6_60Repository);
            _PEC_6_60ByIdSearcher = new PEC_6_60ByIdSearcher(PEC_6_60Repository);
            _PEC_6_60Creator = new PEC_6_60Creator(PEC_6_60Repository);
            _PEC_6_60Updater = new PEC_6_60Updater(PEC_6_60Repository);
            _PEC_6_60SiresoUpdater = new PEC_6_60SIRESO(PEC_6_60Repository);
            _PEC_6_60Deleter = new PEC_6_60Deleter(PEC_6_60Repository);
            _tipoEstadisticaByIdSearcher = new TipoEstadisticaByIdSearcher(tipoEstadisticaRepository);
            _tipoInstrumentoByIdSearcher = new TipoInstrumentoByIdSearcher(instrumentoRepository);
            _entidadFederativaByIdSearcher = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
            _municipioByIdSearcher = new MunicipioByIdSearcher(municipioRepository);
        }

        public PEC_6_60sResponse SearchPEC_6_60ByCriteria(int idTipoEstadistica = 0, int idTipoInstrumento = 0,
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
            return new PEC_6_60sResponse(pec660s.Select(item =>
            {
                var municipio = _municipioByIdSearcher.SearchMunicipioById(item.IdMunicipio);
                var entidadFederativa =
                    _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
                var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(item.IdTipoEstadistica);
                var instrumento = _tipoInstrumentoByIdSearcher.SearchInstrumentoById(item.IdInstrumento);

                return PEC_6_60Response.FromAggregate(item, tipoEstadistica, instrumento, entidadFederativa, municipio);
            }));
        }

        public PEC_6_60Response SearchPEC_6_60ById(int id)
        {
            var pec660 = _PEC_6_60ByIdSearcher.SearchPEC_6_60ById(id);
            var municipio = _municipioByIdSearcher.SearchMunicipioById(pec660.IdMunicipio);
            var entidadFederativa =
                _entidadFederativaByIdSearcher.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
            var tipoEstadistica = _tipoEstadisticaByIdSearcher.SearchTipoEstadisticaById(pec660.IdTipoEstadistica);
            var instrumento = _tipoInstrumentoByIdSearcher.SearchInstrumentoById(pec660.IdInstrumento);

            return PEC_6_60Response.FromAggregate(pec660, tipoEstadistica, instrumento, entidadFederativa, municipio);
        }

        public void RegisterPEC_6_60(int idTipoEstadistica, int idInstrumento, int idMunicipio, string añoEstadistico,
            int mesEstadistico, int consecutivo, byte[] dataArchivo)
        {
            _PEC_6_60Creator.Create(new PEC_6_60AñoEstadistico(añoEstadistico),
                new PEC_6_60MesEstadistico(mesEstadistico), new PEC_6_60Consecutivo(consecutivo), dataArchivo,
                idInstrumento, idTipoEstadistica, idMunicipio);
        }

        public void UpdatePEC_6_60(int id, string añoEstadistico, int mesEstadistico, int consecutivo,
            byte[] dataArchivo,
            int idInstrumento, int idTipoEstadistica, int idMunicipio)
        {
            _PEC_6_60Updater.Update(id, new PEC_6_60AñoEstadistico(añoEstadistico),
                new PEC_6_60MesEstadistico(mesEstadistico), new PEC_6_60Consecutivo(consecutivo), dataArchivo,
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