﻿namespace Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

public class UsuarioPassword
{
    public UsuarioPassword(string password)
    {
        Value = password;
    }

    public string Value { get; }
}