using System;

namespace Organizador_PEC_6_60.Domain.TipoInstrumento.Exceptions;

public class InvalidNombreTipoInstrumento : Exception
{
    public InvalidNombreTipoInstrumento() : base("El nombre del instrumento es inválido.")
    {
    }
}