using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Model;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.TipoEstadistica.Application.Create;
using Organizador_PEC_6_60.TipoEstadistica.Application.Delete;
using Organizador_PEC_6_60.TipoEstadistica.Application.Search;
using Organizador_PEC_6_60.TipoEstadistica.Application.Update;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application
{
    public class ManageTipoEstadistica
    {
        private AllTipoEstadisticaSearcher _allSearcher;
        private TipoEstadisticaByIdSearcher _byIdSearcher;
        private TipoEstadisticaCreator _creator;
        private TipoEstadisticaUpdater _updater;
        private TipoEstadisticaDeleter _deleter;

        public ManageTipoEstadistica(TipoEstadisticaRepository repository)
        {
            _allSearcher = new AllTipoEstadisticaSearcher(repository);
            _byIdSearcher = new TipoEstadisticaByIdSearcher(repository);
            _creator = new TipoEstadisticaCreator(repository);
            _updater = new TipoEstadisticaUpdater(repository);
            _deleter = new TipoEstadisticaDeleter(repository);
        }

        public TiposEstadisticaResponse SearchAllTiposEstadisitca()
        {
            return new TiposEstadisticaResponse(_allSearcher.SearchAllTiposEstadistica());
        }

        public TipoEstadisticaResponse SearchTipoEstadisticaById(int id)
        {
            return TipoEstadisticaResponse.FromAggregate(_byIdSearcher.SearchTipoEstadisticaById(id));
        }

        public void RegisterTipoEstadistica(int clave, string nombre, List<TipoInstrumentoResponse> instrumentos)
        {
            List<TipoInstrumento> listInstrumentos = instrumentos.Select(row =>
                new TipoInstrumento(new TipoInstrumentoNombre(row.Nombre), row.Id)).ToList();

            _creator.Create(new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
        }

        public void UpdateTipoEstadistica(int id, int clave, string nombre, List<TipoInstrumentoResponse> instrumentos)
        {
            List<TipoInstrumento> listInstrumentos = instrumentos.Select(row =>
                new TipoInstrumento(new TipoInstrumentoNombre(row.Nombre), row.Id)).ToList();

            _updater.Update(id, new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
        }

        public void DeleteTipoEstadisitca(int id)
        {
            _deleter.Delete(id);
        }
    }
}