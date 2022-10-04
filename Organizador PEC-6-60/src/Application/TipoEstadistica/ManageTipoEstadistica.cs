using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Application.TipoEstadistica.Create;
using Organizador_PEC_6_60.Application.TipoEstadistica.Delete;
using Organizador_PEC_6_60.Application.TipoEstadistica.Search;
using Organizador_PEC_6_60.Application.TipoEstadistica.Update;
using Organizador_PEC_6_60.Application.TipoInstrumento;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;
using Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoEstadistica
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
            List<Domain.TipoInstrumento.Model.TipoInstrumento> listInstrumentos =
                instrumentos.Select(
                    item =>
                        new Domain.TipoInstrumento.Model.TipoInstrumento(
                            new TipoInstrumentoNombre(item.Nombre),
                            item.Id
                        )
                ).ToList();

            _creator.Create(new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
        }

        public void UpdateTipoEstadistica(
            int id,
            int clave,
            string nombre,
            List<TipoInstrumentoResponse> instrumentos
        )
        {
            List<Domain.TipoInstrumento.Model.TipoInstrumento> listInstrumentos =
                instrumentos.Select(
                    item =>
                        new Domain.TipoInstrumento.Model.TipoInstrumento(
                            new TipoInstrumentoNombre(item.Nombre),
                            item.Id
                        )
                ).ToList();

            _updater.Update(id, new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
        }

        public void DeleteTipoEstadisitca(int id)
        {
            _deleter.Delete(id);
        }
    }
}