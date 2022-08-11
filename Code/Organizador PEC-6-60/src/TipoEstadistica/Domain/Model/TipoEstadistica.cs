using System.Collections.Generic;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Model
{
    public class TipoEstadistica
    {
        #region Properties

        public int Id { get; }
        public TipoEstadisticaClave Clave { get; }
        public TipoEstadisticaNombre Nombre { get; }
        public List<Instrumento.Domain.Model.Instrumento> Instrumentos { get; }

        #endregion

        #region Constructors

        public TipoEstadistica(TipoEstadisticaClave clave, TipoEstadisticaNombre nombre,
            List<Instrumento.Domain.Model.Instrumento> instrumentos, int id = 0)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
            Instrumentos = instrumentos;
        }
        
        #endregion
    }
}