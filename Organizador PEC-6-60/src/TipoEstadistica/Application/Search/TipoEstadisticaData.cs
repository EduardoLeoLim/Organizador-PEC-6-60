using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;
using Organizador_PEC_6_60.TipoInstrumento.Application;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class TipoEstadisticaData
{
    public TipoEstadisticaData(
        int id,
        TipoEstadisticaClave clave,
        TipoEstadisticaNombre nombre,
        IEnumerable<TipoInstrumento.Domain.Model.TipoInstrumento> instrumentos)
    {
        Id = id;
        Clave = clave.Value;
        Nombre = nombre.Value;
        Instrumentos = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
    }

    public int Id { get; }
    public int Clave { get; }
    public string Nombre { get; }
    public IEnumerable<TipoInstrumentoResponse> Instrumentos { get; }

    public static TipoEstadisticaData FromAggregate(Domain.Model.TipoEstadistica tipoEstadistica)
    {
        return new TipoEstadisticaData(
            tipoEstadistica.Id,
            tipoEstadistica.Clave,
            tipoEstadistica.Nombre,
            tipoEstadistica.Instrumentos
        );
    }

    public override string ToString()
    {
        return $"{Clave:000} - {Nombre}";
    }
}