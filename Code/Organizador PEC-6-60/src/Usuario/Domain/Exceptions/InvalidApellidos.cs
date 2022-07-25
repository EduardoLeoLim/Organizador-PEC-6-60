using System;

namespace Organizador_PEC_6_60.Usuario.Domain.Exceptions
{
    public class InvalidApellidos : Exception
    {
        public InvalidApellidos() : base("Apellidos inválidos.")
        {
            
        }
    }
}