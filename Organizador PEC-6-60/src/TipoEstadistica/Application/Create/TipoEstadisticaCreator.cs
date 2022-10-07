using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Exceptions;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Create;

public class TipoEstadisticaCreator
{
    private readonly TipoEstadisticaRepository _repository;

    public TipoEstadisticaCreator(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public void Create(
        TipoEstadisticaClave clave,
        TipoEstadisticaNombre nombre,
        List<TipoInstrumento.Domain.Model.TipoInstrumento> instrumentos
    )
    {
        if (!IsValid(clave))
            throw new InvalidClaveTipoEstadistica();
        if (!IsValid(nombre))
            throw new InvalidNombreTipoEstadistica();
        if (!IsValid(instrumentos))
            throw new InvalidInstrumentosTipoEstadistica();

        _repository.Insert(new Domain.Model.TipoEstadistica(clave, nombre, instrumentos));
    }

    private bool IsValid(object obj)
    {
        if (obj is TipoEstadisticaClave)
        {
            var clave = ((TipoEstadisticaClave)obj).Value;
            if (clave <= 0)
                return false;
            //Add more validations here

            return true;
        }

        if (obj is TipoEstadisticaNombre)
        {
            var nombre = ((TipoEstadisticaNombre)obj).Value;

            if (string.IsNullOrEmpty(nombre))
                return false;
            if (nombre.Trim().Length == 0)
                return false;
            //Add more validations here

            return true;
        }

        if (obj is List<TipoInstrumento.Domain.Model.TipoInstrumento>)
        {
            var list =
                (List<TipoInstrumento.Domain.Model.TipoInstrumento>)obj;

            if (list.Count == 0)
                return false;

            return true;
        }

        return false;
    }
}