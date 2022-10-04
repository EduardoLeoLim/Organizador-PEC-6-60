using Organizador_PEC_6_60.Application.TipoInstrumento.Create;
using Organizador_PEC_6_60.Application.TipoInstrumento.Delete;
using Organizador_PEC_6_60.Application.TipoInstrumento.Search;
using Organizador_PEC_6_60.Application.TipoInstrumento.Update;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoInstrumento
{
    public class ManageTiposInstrumento
    {
        private AllTipoInstrumentoSearcher _allSearcher;
        private TipoInstrumentoByIdSearcher _byIdSearcher;
        private TipoInstrumentoCreator _creator;
        private TipoInstrumentoUpdater _updater;
        private TipoInstrumentoDeleter _deleter;

        public ManageTiposInstrumento(TipoInstrumentoRepository repository)
        {
            _allSearcher = new AllTipoInstrumentoSearcher(repository);
            _byIdSearcher = new TipoInstrumentoByIdSearcher(repository);
            _creator = new TipoInstrumentoCreator(repository);
            _updater = new TipoInstrumentoUpdater(repository);
            _deleter = new TipoInstrumentoDeleter(repository);
        }

        public TiposInstrumentoResponse SearchAllInstrumentos()
        {
            return new TiposInstrumentoResponse(_allSearcher.SearchAllInstrumentos());
        }

        public TipoInstrumentoResponse SearchInstrumentoById(int id)
        {
            return TipoInstrumentoResponse.FromAggregate(_byIdSearcher.SearchTipoInstrumentoById(id));
        }

        public void RegisterInstrumento(string nombre)
        {
            _creator.Create(new TipoInstrumentoNombre(nombre));
        }

        public void UpdateInstrumento(int id, string nombre)
        {
            _updater.Update(id, new TipoInstrumentoNombre(nombre));
        }

        public void DeleteInstrumento(int id)
        {
            _deleter.Delete(id);
        }
    }
}