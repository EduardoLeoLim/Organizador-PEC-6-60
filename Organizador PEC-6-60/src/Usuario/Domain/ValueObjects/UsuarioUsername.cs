namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

public class UsuarioUsername
{
    public UsuarioUsername(string username)
    {
        Value = username;
    }

    public string Value { get; }
}