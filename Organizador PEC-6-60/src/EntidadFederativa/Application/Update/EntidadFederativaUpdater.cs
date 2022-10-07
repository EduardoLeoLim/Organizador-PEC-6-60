using Organizador_PEC_6_60.EntidadFederativa.Domain.Exceptions;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Update;

public class EntidadFederativaUpdater : EntidadFederativaUpdaterService
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

        _repository.Update(new Domain.Model.EntidadFederativa(clave, nombre, id));
    }

    private bool IsValid(object obj)
    {
        if (obj is EntidadFederativaClave)
        {
            var clave = ((EntidadFederativaClave)obj).Value;
            if (clave <= 0)
                return false;
            //Add more validations here

            return true;
        }

        if (obj is EntidadFederativaNombre)
        {
            var nombre = ((EntidadFederativaNombre)obj).Value;

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