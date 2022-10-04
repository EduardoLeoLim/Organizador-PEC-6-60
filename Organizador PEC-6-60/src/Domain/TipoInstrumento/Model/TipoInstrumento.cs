using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Domain.TipoInstrumento.Model
{
    public class TipoInstrumento
    {
        public int Id { get; }
        public TipoInstrumentoNombre Nombre { get; }

        public TipoInstrumento(TipoInstrumentoNombre nombre, int id = 0)
        {
            Nombre = nombre;
            Id = id;
        }
    }
}