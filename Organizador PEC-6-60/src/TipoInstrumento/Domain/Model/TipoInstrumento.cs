using Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoInstrumento.Domain.Model;

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