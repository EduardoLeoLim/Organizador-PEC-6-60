using System;

namespace Organizador_PEC_6_60.Usuario.Domain.Exceptions
{
    public class InvalidNombre : Exception
    {
        public InvalidNombre() : base("Nombre Inválido.")
        {
            
        }
    }
}