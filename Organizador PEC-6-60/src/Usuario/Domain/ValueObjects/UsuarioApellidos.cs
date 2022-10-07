namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

public class UsuarioApellidos
{
    public UsuarioApellidos(string apelidos)
    {
        Value = apelidos;
    }

    public string Value { get; }
}