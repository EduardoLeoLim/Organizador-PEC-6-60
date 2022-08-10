using System;

namespace Organizador_PEC_6_60.Instrumento.Domain.Exceptions;

public class InvalidNombreInstrumento : Exception
{
    public InvalidNombreInstrumento() : base("El nombre del instrumento es inválido.") {}
}