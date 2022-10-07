using System;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Exceptions;

public class InvalidClaveTipoEstadistica : Exception
{
    public InvalidClaveTipoEstadistica() : base("El formato de la clave es inváldo.")
    {
    }
}