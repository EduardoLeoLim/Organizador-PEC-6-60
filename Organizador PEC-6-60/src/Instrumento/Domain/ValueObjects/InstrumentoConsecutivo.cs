namespace Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

public class InstrumentoConsecutivo
{
    public InstrumentoConsecutivo(int consecutivo)
    {
        Value = consecutivo;
    }

    public int Value { get; }
}