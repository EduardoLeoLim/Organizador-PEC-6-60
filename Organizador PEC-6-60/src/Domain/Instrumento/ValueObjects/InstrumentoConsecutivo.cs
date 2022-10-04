namespace Organizador_PEC_6_60.Domain.Instrumento.ValueObjects
{
    public class InstrumentoConsecutivo
    {
        public int Value { get; }

        public InstrumentoConsecutivo(int consecutivo)
        {
            Value = consecutivo;
        }
    }
}