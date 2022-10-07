using System;

namespace Organizador_PEC_6_60.TipoInstrumento.Domain.Exceptions;

public class InvalidNombreTipoInstrumento : Exception
{
    public InvalidNombreTipoInstrumento() : base("El nombre del instrumento es inválido.")
    {
    }
}