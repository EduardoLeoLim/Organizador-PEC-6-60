using System;

namespace Organizador_PEC_6_60.Domain.Usuario.Exceptions
{
    public class InvalidNombre : Exception
    {
        public InvalidNombre() : base("Nombre Inválido.")
        {
        }
    }
}