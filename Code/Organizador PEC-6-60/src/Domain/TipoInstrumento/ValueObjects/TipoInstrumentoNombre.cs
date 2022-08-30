namespace Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects
{
    public class TipoInstrumentoNombre
    {
        public string Value { get; }

        public TipoInstrumentoNombre(string nombre)
        {
            Value = nombre;
        }
    }
}