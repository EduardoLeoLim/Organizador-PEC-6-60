using System;

namespace Organizador_PEC_6_60.Domain.Usuario.Exceptions;

public class InvalidApellidos : Exception
{
    public InvalidApellidos() : base("Apellidos inválidos.")
    {
    }
}