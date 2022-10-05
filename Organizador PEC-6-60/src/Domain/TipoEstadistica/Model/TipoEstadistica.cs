using System.Collections.Generic;
using Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;

namespace Organizador_PEC_6_60.Domain.TipoEstadistica.Model;

public class TipoEstadistica
{
    public TipoEstadistica(
        TipoEstadisticaClave clave,
        TipoEstadisticaNombre nombre,
        List<TipoInstrumento.Model.TipoInstrumento> instrumentos,
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
    public IEnumerable<TipoInstrumento.Model.TipoInstrumento> Instrumentos { get; }
}