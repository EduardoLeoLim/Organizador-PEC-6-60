﻿using Organizador_PEC_6_60.Municipio.Domain.Exceptions;
using Organizador_PEC_6_60.Municipio.Domain.Repository;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Update
{
    public class MunicipioUpdater
    {
        private readonly MunicipioRepository _repository;

        public MunicipioUpdater(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public void Update(int id, MunicipioClave clave, MunicipioNombre nombre,
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            if (!IsValid(clave))
                throw new InvalidClaveMunicipio();
            if (!IsValid(nombre))
                throw new InvalidNombreMunicipio();

            _repository.Update(new Domain.Model.Municipio(clave, nombre, entidadFederativa.Id, id));
        }

        private bool IsValid(object obj)
        {
            if (obj is MunicipioClave)
            {
                int clave = ((MunicipioClave)obj).Value;
                if (clave <= 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is MunicipioNombre)
            {
                string nombre = ((MunicipioNombre)obj).Value;

                if (string.IsNullOrEmpty(nombre))
                    return false;
                if (nombre.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            return false;
        }
    }
}