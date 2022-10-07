namespace Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

public class MunicipioClave
{
    public MunicipioClave(int id)
    {
        Value = id;
    }

    public int Value { get; }
}