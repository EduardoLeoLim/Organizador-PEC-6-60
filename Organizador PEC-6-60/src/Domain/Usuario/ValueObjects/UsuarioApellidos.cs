namespace Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

public class UsuarioApellidos
{
    public UsuarioApellidos(string apelidos)
    {
        Value = apelidos;
    }

    public string Value { get; }
}