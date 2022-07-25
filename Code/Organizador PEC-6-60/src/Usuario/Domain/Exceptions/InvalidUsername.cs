using System;

namespace Organizador_PEC_6_60.Usuario.Domain.Exceptions
{
    public class InvalidUsername : Exception
    {
        public InvalidUsername() : base("Nombre de usuario inválido.")
        {
        
        }
    }
}