﻿namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

public class UsuarioNombre
{
    public UsuarioNombre(string nombre)
    {
        Value = nombre;
    }

    public string Value { get; }
}