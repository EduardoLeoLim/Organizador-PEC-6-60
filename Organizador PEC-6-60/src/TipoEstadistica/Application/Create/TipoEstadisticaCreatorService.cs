using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Create;

public interface TipoEstadisticaCreatorService
{
    void Create(TipoEstadisticaClave clave, TipoEstadisticaNombre nombre,
        List<TipoInstrumento.Domain.Model.TipoInstrumento> instrumentos);
}