using Organizador_PEC_6_60.Domain.EntidadFederativa.Exceptions;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Update
{
    public class EntidadFederativaUpdater
    {
        private readonly EntidadFederativaRepository _repository;

        public EntidadFederativaUpdater(EntidadFederativaRepository repository)
        {
            _repository = repository;
        }

        public void Update(int id, EntidadFederativaClave clave, EntidadFederativaNombre nombre)
        {
            if (!IsValid(clave))
                throw new InvalidClaveEntidadFederativa();
            if (!IsValid(nombre))
                throw new InvalidNombreEntidadFederativa();

            _repository.Update(new Domain.EntidadFederativa.Model.EntidadFederativa(clave, nombre, id));
        }

        private bool IsValid(object obj)
        {
            if (obj is EntidadFederativaClave)
            {
                int clave = ((EntidadFederativaClave)obj).Value;
                if (clave <= 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is EntidadFederativaNombre)
            {
                string nombre = ((EntidadFederativaNombre)obj).Value;

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