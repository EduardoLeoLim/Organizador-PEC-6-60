using Organizador_PEC_6_60.Domain.Municipio.Exceptions;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Update;

public class MunicipioUpdater : MunicipioUpdaterService
{
    private readonly MunicipioRepository _repository;

    public MunicipioUpdater(MunicipioRepository repository)
    {
        _repository = repository;
    }

    public void Update(
        int id,
        MunicipioClave clave,
        MunicipioNombre nombre,
        Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa
    )
    {
        if (!IsValid(clave))
            throw new InvalidClaveMunicipio();
        if (!IsValid(nombre))
            throw new InvalidNombreMunicipio();

        _repository.Update(new Domain.Municipio.Model.Municipio(clave, nombre, entidadFederativa.Id, id));
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