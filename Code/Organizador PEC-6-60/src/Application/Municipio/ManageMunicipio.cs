﻿using Organizador_PEC_6_60.Application.EntidadFederativa;
using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio.Create;
using Organizador_PEC_6_60.Application.Municipio.Delete;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;
using Organizador_PEC_6_60.Municipio.Application.Update;

namespace Organizador_PEC_6_60.Application.Municipio
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
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa =
                new Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa(
                    new EntidadFederativaClave(entidadFederativaResponse.Clave),
                    new EntidadFederativaNombre(entidadFederativaResponse.Nombre), entidadFederativaResponse.Id);
            _creator.Create(new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
        }

        public void UpdateMunicipio(int id, int clave, string nombre,
            EntidadFederativaResponse entidadFederativaResponse)
        {
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa =
                new Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa(
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