namespace Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

public class UsuarioUsername
{
    public UsuarioUsername(string username)
    {
        Value = username;
    }

    public string Value { get; }
}