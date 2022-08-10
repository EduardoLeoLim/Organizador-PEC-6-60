using Organizador_PEC_6_60.Instrumento.Application.Create;
using Organizador_PEC_6_60.Instrumento.Application.Delete;
using Organizador_PEC_6_60.Instrumento.Application.Search;
using Organizador_PEC_6_60.Instrumento.Application.Update;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Application
{
    public class ManageInstrumento
    {
        private AllInstrumentoSearcher _allSearcher;
        private InstrumentoByIdSearcher _byIdSearcher;
        private InstrumentoCreator _creator;
        private InstrumentoUpdater _updater;
        private InstrumentoDeleter _deleter;

        public ManageInstrumento(InstrumentoRepository repository)
        {
            _allSearcher = new AllInstrumentoSearcher(repository);
            _byIdSearcher = new InstrumentoByIdSearcher(repository);
            _creator = new InstrumentoCreator(repository);
            _updater = new InstrumentoUpdater(repository);
            _deleter = new InstrumentoDeleter(repository);
        }

        public InstrumentosResponse SearchAllInstrumentos()
        {
            return new InstrumentosResponse(_allSearcher.SearchAllInstrumentos());
        }

        public InstrumentoResponse SearchInstrumentoById(int id)
        {
            return InstrumentoResponse.FromAggregate(_byIdSearcher.SearchInstrumentoById(id));
        }

        public void RegisterInstrumento(string nombre)
        {
            _creator.Create(new InstrumentoNombre(nombre));
        }

        public void UpdateInstrumento(int id, string nombre)
        {
            _updater.Update(id, new InstrumentoNombre(nombre));
        }

        public void DeleteInstrumento(int id)
        {
            _deleter.Delete(id);
        }
    }
}