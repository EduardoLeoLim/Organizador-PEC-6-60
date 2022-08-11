using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application
{
    public class TipoEstadisticaResponse
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }
        public IEnumerable<InstrumentoResponse> Instrumentos { get; }

        public TipoEstadisticaResponse(int id, TipoEstadisticaClave clave, TipoEstadisticaNombre nombre,
            IEnumerable<Instrumento.Domain.Model.Instrumento> instrumentos)
        {
            Id = id;
            Clave = clave.Value;
            Nombre = nombre.Value;
            Instrumentos = instrumentos.Select(row => InstrumentoResponse.FromAggregate(row));
        }

        public static TipoEstadisticaResponse FromAggregate(Domain.Model.TipoEstadistica tipoEstadistica)
        {
            return new TipoEstadisticaResponse(tipoEstadistica.Id, tipoEstadistica.Clave, tipoEstadistica.Nombre,
                tipoEstadistica.Instrumentos);
        }
    }
}