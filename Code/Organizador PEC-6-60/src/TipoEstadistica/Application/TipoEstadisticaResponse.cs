using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Model;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application
{
    public class TipoEstadisticaResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public IEnumerable<TipoInstrumentoResponse> Instrumentos { get; }

        public TipoEstadisticaResponse(int id, TipoEstadisticaClave clave, TipoEstadisticaNombre nombre,
            IEnumerable<TipoInstrumento> instrumentos)
        {
            Id = id;
            Clave = clave.Value;
            Nombre = nombre.Value;
            Instrumentos = instrumentos.Select(row => TipoInstrumentoResponse.FromAggregate(row));
        }

        public static TipoEstadisticaResponse FromAggregate(Domain.Model.TipoEstadistica tipoEstadistica)
        {
            return new TipoEstadisticaResponse(tipoEstadistica.Id, tipoEstadistica.Clave, tipoEstadistica.Nombre,
                tipoEstadistica.Instrumentos);
        }

        public override string ToString()
        {
            return $"{Clave:000} - {Nombre}";
        }
    }
}