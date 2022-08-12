using System;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.Exceptions
{
    public class InvalidConsecutivoPEC_6_60 : Exception
    {
        public InvalidConsecutivoPEC_6_60() : base("El formato del consecutivo no es válido.") {}
        public InvalidConsecutivoPEC_6_60(string message) : base(message) {}
    }
}