﻿namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioPassword
    {
        public string Value { get; } 
        
        public UsuarioPassword(string password)
        {
            Value = password;
        }
    }
}