using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Application.TipoInstrumento;
using Organizador_PEC_6_60.Domain.TipoEstadistica.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoEstadistica
{
    public class TipoEstadisticaResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public IEnumerable<TipoInstrumentoResponse> Instrumentos { get; }

        public TipoEstadisticaResponse(
            int id,
            TipoEstadisticaClave clave,
            TipoEstadisticaNombre nombre,
            IEnumerable<Domain.TipoInstrumento.Model.TipoInstrumento> instrumentos)
        {
            Id = id;
            Clave = clave.Value;
            Nombre = nombre.Value;
            Instrumentos = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
        }

        public static TipoEstadisticaResponse FromAggregate(
            Domain.TipoEstadistica.Model.TipoEstadistica tipoEstadistica)
        {
            return new TipoEstadisticaResponse(
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
}