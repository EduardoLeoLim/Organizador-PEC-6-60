namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

public class UsuarioPassword
{
    public UsuarioPassword(string password)
    {
        Value = password;
    }

    public string Value { get; }
}