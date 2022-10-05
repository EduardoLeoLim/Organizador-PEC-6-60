namespace Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

public class MunicipioClave
{
    public MunicipioClave(int id)
    {
        Value = id;
    }

    public int Value { get; }
}