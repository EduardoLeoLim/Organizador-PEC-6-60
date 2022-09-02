using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Exceptions;
using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;
using Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoEstadistica.Create
{
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
            List<Domain.TipoInstrumento.Model.TipoInstrumento> instrumentos
        )
        {
            if (!IsValid(clave))
                throw new InvalidClaveTipoEstadistica();
            if (!IsValid(nombre))
                throw new InvalidNombreTipoEstadistica();
            if (!IsValid(instrumentos))
                throw new InvalidInstrumentosTipoEstadistica();

            _repository.Insert(new Domain.TipoEstadistica.Model.TipoEstadistica(clave, nombre, instrumentos));
        }

        private bool IsValid(object obj)
        {
            if (obj is TipoEstadisticaClave)
            {
                int clave = ((TipoEstadisticaClave)obj).Value;
                if (clave <= 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is TipoEstadisticaNombre)
            {
                string nombre = ((TipoEstadisticaNombre)obj).Value;

                if (string.IsNullOrEmpty(nombre))
                    return false;
                if (nombre.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            if (obj is List<Domain.TipoInstrumento.Model.TipoInstrumento>)
            {
                List<Domain.TipoInstrumento.Model.TipoInstrumento> list =
                    (List<Domain.TipoInstrumento.Model.TipoInstrumento>)obj;

                if (list.Count == 0)
                    return false;

                return true;
            }

            return false;
        }
    }
}