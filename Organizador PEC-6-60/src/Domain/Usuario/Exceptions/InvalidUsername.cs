using System;

namespace Organizador_PEC_6_60.Domain.Usuario.Exceptions
{
    public class InvalidUsername : Exception
    {
        public InvalidUsername() : base("Nombre de usuario inválido.")
        {
        }
    }
}