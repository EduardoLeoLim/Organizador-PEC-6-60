using Organizador_PEC_6_60.Domain.Municipio.Exceptions;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Create
{
    public class MunicipioCreator
    {
        private readonly MunicipioRepository _repository;

        public MunicipioCreator(MunicipioRepository repository)
        {
            _repository = repository;
        }

        public void Create(MunicipioClave clave, MunicipioNombre nombre,
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            if (!IsValid(clave))
                throw new InvalidClaveMunicipio();
            if (!IsValid(nombre))
                throw new InvalidNombreMunicipio();

            _repository.Insert(new Domain.Municipio.Model.Municipio(clave, nombre, entidadFederativa.Id));
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