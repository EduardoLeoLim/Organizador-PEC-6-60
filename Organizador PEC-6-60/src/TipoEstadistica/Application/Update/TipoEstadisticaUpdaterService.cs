using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Update;

public interface TipoEstadisticaUpdaterService
{
    public void Update(int id, TipoEstadisticaClave clave, TipoEstadisticaNombre nombre,
        List<TipoInstrumento.Domain.Model.TipoInstrumento> instrumentos);
}