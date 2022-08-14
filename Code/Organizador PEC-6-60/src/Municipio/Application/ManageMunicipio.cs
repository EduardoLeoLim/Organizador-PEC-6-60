using Organizador_PEC_6_60.EntidadFederativa.Application;
using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Municipio.Application.Create;
using Organizador_PEC_6_60.Municipio.Application.Delete;
using Organizador_PEC_6_60.Municipio.Application.Search;
using Organizador_PEC_6_60.Municipio.Application.Update;
using Organizador_PEC_6_60.Municipio.Domain.Repository;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application
{
    public class ManageMunicipio
    {
        private AllMunicipioSeacher _allSearcher;
        private MunicipioByIdSearcher _byIdSearcher;
        private EntidadFederativaByIdSearcher _byIdEntidadFederativaSearcer;
        private MunicipioCreator _creator;
        private MunicipioUpdater _updater;
        private MunicipioDeleter _deleter;

        public ManageMunicipio(MunicipioRepository municipioRepository,
            EntidadFederativaRepository entidadFederativaRepository)
        {
            _allSearcher = new AllMunicipioSeacher(municipioRepository);
            _byIdSearcher = new MunicipioByIdSearcher(municipioRepository);
            _byIdEntidadFederativaSearcer = new EntidadFederativaByIdSearcher(entidadFederativaRepository);
            _creator = new MunicipioCreator(municipioRepository);
            _updater = new MunicipioUpdater(municipioRepository);
            _deleter = new MunicipioDeleter(municipioRepository);
        }

        public MunicipiosResponse SearchAllMunicipios(int idEntidadFederativa)
        {
            return new MunicipiosResponse(_allSearcher.SearchAllMunicipios(idEntidadFederativa),
                _byIdEntidadFederativaSearcer.SearchEntidadFederativaById(idEntidadFederativa));
        }

        public MunicipioResponse SearchMunicipioById(int id)
        {
            var municipio = _byIdSearcher.SearchMunicipioById(id);
            var entidadFederativa =
                _byIdEntidadFederativaSearcer.SearchEntidadFederativaById(municipio.IdEntidadFederativa);
            return MunicipioResponse.FromAggregate(municipio, entidadFederativa);
        }

        public void RegisterMunicipio(int clave, string nombre, EntidadFederativaResponse entidadFederativaResponse)
        {
            EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa =
                new EntidadFederativa.Domain.Model.EntidadFederativa(
                    new EntidadFederativaClave(entidadFederativaResponse.Clave),
                    new EntidadFederativaNombre(entidadFederativaResponse.Nombre), entidadFederativaResponse.Id);
            _creator.Create(new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
        }

        public void UpdateMunicipio(int id, int clave, string nombre,
            EntidadFederativaResponse entidadFederativaResponse)
        {
            EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa =
                new EntidadFederativa.Domain.Model.EntidadFederativa(
                    new EntidadFederativaClave(entidadFederativaResponse.Clave),
                    new EntidadFederativaNombre(entidadFederativaResponse.Nombre), entidadFederativaResponse.Id);
            _updater.Update(id, new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
        }

        public void DeleteMunicipio(int id)
        {
            _deleter.Delete(id);
        }
    }
}