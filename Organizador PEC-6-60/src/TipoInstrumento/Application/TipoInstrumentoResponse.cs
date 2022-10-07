namespace Organizador_PEC_6_60.TipoInstrumento.Application;

public class TipoInstrumentoResponse
{
    public TipoInstrumentoResponse(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public int Id { get; }
    public string Nombre { get; }

    public static TipoInstrumentoResponse FromAggregate(Domain.Model.TipoInstrumento tipoInstrumento)
    {
        return new TipoInstrumentoResponse(tipoInstrumento.Id, tipoInstrumento.Nombre.Value);
    }

    public override string ToString()
    {
        return Nombre;
    }
}