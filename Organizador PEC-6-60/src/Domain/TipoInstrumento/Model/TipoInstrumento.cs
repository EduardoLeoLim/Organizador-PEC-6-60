using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Domain.TipoInstrumento.Model;

public class TipoInstrumento
{
    public TipoInstrumento(TipoInstrumentoNombre nombre, int id = 0)
    {
        Nombre = nombre;
        Id = id;
    }

    public int Id { get; }
    public TipoInstrumentoNombre Nombre { get; }
}