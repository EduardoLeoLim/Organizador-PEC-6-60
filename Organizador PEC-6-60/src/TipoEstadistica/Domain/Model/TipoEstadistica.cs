using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Model;

public class TipoEstadistica
{
    public TipoEstadistica(
        TipoEstadisticaClave clave,
        TipoEstadisticaNombre nombre,
        List<TipoInstrumento.Domain.Model.TipoInstrumento> instrumentos,
        int id = 0
    )
    {
        Id = id;
        Clave = clave;
        Nombre = nombre;
        Instrumentos = instrumentos;
    }

    public int Id { get; }
    public TipoEstadisticaClave Clave { get; }
    public TipoEstadisticaNombre Nombre { get; }
    public IEnumerable<TipoInstrumento.Domain.Model.TipoInstrumento> Instrumentos { get; }
}