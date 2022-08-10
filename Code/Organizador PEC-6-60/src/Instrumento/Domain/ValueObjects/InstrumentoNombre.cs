namespace Organizador_PEC_6_60.Instrumento.Domain.ValueObjects
{
    public class InstrumentoNombre
    {
        public string Value { get; }

        public InstrumentoNombre(string nombre)
        {
            Value = nombre;
        }
    }
}