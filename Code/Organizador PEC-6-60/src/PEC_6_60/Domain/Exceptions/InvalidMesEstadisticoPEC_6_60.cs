using System;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.Exceptions
{
    public class InvalidMesEstadisticoPEC_6_60 : Exception
    {
        public InvalidMesEstadisticoPEC_6_60() : base("La clave del mes estadístico no es válida.") {}
        public InvalidMesEstadisticoPEC_6_60(string message) : base(message) {}
    }
}