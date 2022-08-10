using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Domain.Model
{
    public class Instrumento
    {
        #region Properties

        public int Id { get; }
        public InstrumentoNombre Nombre { get; }
        
        #endregion

        #region Constructors

        public Instrumento(InstrumentoNombre nombre, int id = 0)
        {
            Nombre = nombre;
            Id = id;
        }

        #endregion
        
    }
}