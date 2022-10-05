using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Exceptions;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;
using Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoEstadistica.Update;

public class TipoEstadisticaUpdater
{
    private readonly TipoEstadisticaRepository _repository;

    public TipoEstadisticaUpdater(TipoEstadisticaRepository repository)
    {
        _repository = repository;
    }

    public void Update(
        int id,
        TipoEstadisticaClave clave,
        TipoEstadisticaNombre nombre,
        List<Domain.TipoInstrumento.Model.TipoInstrumento> instrumentos
    )
    {
        if (!IsValid(clave))
            throw new InvalidClaveTipoEstadistica();
        if (!IsValid(nombre))
            throw new InvalidNombreTipoEstadistica();
        if (!IsValid(instrumentos))
            throw new InvalidInstrumentosTipoEstadistica();

        _repository.Update(new Domain.TipoEstadistica.Model.TipoEstadistica(clave, nombre, instrumentos, id));
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

        if (obj is List<Domain.TipoInstrumento.Model.TipoInstrumento>)
        {
            var list =
                (List<Domain.TipoInstrumento.Model.TipoInstrumento>)obj;

            if (list.Count == 0)
                return false;

            return true;
        }

        return false;
    }
}