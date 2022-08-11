using System;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Exceptions
{
    public class InvalidInstrumentosTipoEstadistica : Exception
    {
        public InvalidInstrumentosTipoEstadistica() : base("La lista de instrumentos en inválida") {}
        
        public InvalidInstrumentosTipoEstadistica(string errorMessage) : base(errorMessage) {}
    }
}