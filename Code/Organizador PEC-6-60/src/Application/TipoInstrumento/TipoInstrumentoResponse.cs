namespace Organizador_PEC_6_60.Application.TipoInstrumento
{
    public class TipoInstrumentoResponse
    {
        public int Id { get; }
        public string Nombre { get; }

        public TipoInstrumentoResponse(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public static TipoInstrumentoResponse FromAggregate(Domain.TipoInstrumento.Model.TipoInstrumento tipoInstrumento)
        {
            return new TipoInstrumentoResponse(tipoInstrumento.Id, tipoInstrumento.Nombre.Value);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}