using System;

namespace Organizador_PEC_6_60.Instrumento.Domain.Exceptions;

public class InvalidMesEstadisticoInstrumento : Exception
{
    public InvalidMesEstadisticoInstrumento() : base("La clave del mes estadístico no es válida.")
    {
    }

    public InvalidMesEstadisticoInstrumento(string message) : base(message)
    {
    }
}