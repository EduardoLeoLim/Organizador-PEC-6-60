using System;

namespace Organizador_PEC_6_60.Domain.Usuario.Exceptions;

public class InvalidPassword : Exception
{
    public InvalidPassword() : base("Formato de contraseña inválido.")
    {
    }

    public InvalidPassword(string message) : base(message)
    {
    }
}