using Organizador_PEC_6_60.Application.EntidadFederativa.Create;
using Organizador_PEC_6_60.Application.EntidadFederativa.Delete;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.EntidadFederativa.Update;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa
{
    public class ManageEntidadFederativa
    {
        private AllEntidadFederativaSearcher _allSeacher;
        private EntidadFederativaByIdSearcher _byIdSearcher;
        private EntidadFederativaCreator _creator;
        private EntidadFederativaUpdater _updater;
        private EntidadFederativaDeleter _deleter;

        public ManageEntidadFederativa(EntidadFederativaRepository repository)
        {
            _creator = new EntidadFederativaCreator(repository);
            _deleter = new EntidadFederativaDeleter(repository);
            _updater = new EntidadFederativaUpdater(repository);
            _allSeacher = new AllEntidadFederativaSearcher(repository);
            _byIdSearcher = new EntidadFederativaByIdSearcher(repository);
        }

        public EntidadesFederativasResponse SearchAllEntidadesFederativas()
        {
            return new EntidadesFederativasResponse(_allSeacher.SearchAllEntidadesFederativas());
        }

        public EntidadFederativaResponse SearchEntidadFederativaById(int id)
        {
            return EntidadFederativaResponse.FromAggregate(_byIdSearcher.SearchEntidadFederativaById(id));
        }

        public void RegisterEntidadFederativa(int clave, string nombre)
        {
            _creator.Create(new EntidadFederativaClave(clave), new EntidadFederativaNombre(nombre));
        }

        public void UpdateEntidadFederativa(int id, int clave, string nombre)
        {
            _updater.Update(id, new EntidadFederativaClave(clave), new EntidadFederativaNombre(nombre));
        }

        public void DeleteEntidadfederativa(int id)
        {
            _deleter.Delete(id);
        }
    }
}