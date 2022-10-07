using System;

namespace Organizador_PEC_6_60.Instrumento.Domain.Exceptions;

public class InvalidAñoEstadisticoInareumento : Exception
{
    public InvalidAñoEstadisticoInareumento() : base("El año seleccionado es inválido.")
    {
    }

    public InvalidAñoEstadisticoInareumento(string message) : base(message)
    {
    }
}