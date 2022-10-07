using Organizador_PEC_6_60.Municipio.Domain.Exceptions;
using Organizador_PEC_6_60.Municipio.Domain.Repository;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Create;

public class MunicipioCreator : MunicipioCreatorService
{
    private readonly MunicipioRepository _repository;

    public MunicipioCreator(MunicipioRepository repository)
    {
        _repository = repository;
    }

    public void Create(MunicipioClave clave, MunicipioNombre nombre,
        EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa)
    {
        if (!IsValid(clave))
            throw new InvalidClaveMunicipio();
        if (!IsValid(nombre))
            throw new InvalidNombreMunicipio();

        _repository.Insert(new Domain.Model.Municipio(clave, nombre, entidadFederativa.Id));
    }

    private bool IsValid(object obj)
    {
        if (obj is MunicipioClave)
        {
            var clave = ((MunicipioClave)obj).Value;
            if (clave <= 0)
                return false;
            //Add more validations here

            return true;
        }

        if (obj is MunicipioNombre)
        {
            var nombre = ((MunicipioNombre)obj).Value;

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