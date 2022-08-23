namespace Organizador_PEC_6_60.Instrumento.Application
{
    public class InstrumentoResponse
    {
        public int Id { get; }
        public string Nombre { get; }

        public InstrumentoResponse(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public static InstrumentoResponse FromAggregate(Domain.Model.Instrumento instrumento)
        {
            return new InstrumentoResponse(instrumento.Id, instrumento.Nombre.Value);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}