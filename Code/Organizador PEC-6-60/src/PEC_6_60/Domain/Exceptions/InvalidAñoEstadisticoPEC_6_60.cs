using System;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.Exceptions;

public class InvalidAñoEstadisticoPEC_6_60 : Exception
{
    public InvalidAñoEstadisticoPEC_6_60() : base("El año seleccionado es inválido.") {}
    public InvalidAñoEstadisticoPEC_6_60(string message) : base(message) {}
}