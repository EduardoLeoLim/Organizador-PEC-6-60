﻿namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioUsername
    {
        public string Value { get; }
    
        public UsuarioUsername(string username)
        {
            Value = username;
        }
    }
}