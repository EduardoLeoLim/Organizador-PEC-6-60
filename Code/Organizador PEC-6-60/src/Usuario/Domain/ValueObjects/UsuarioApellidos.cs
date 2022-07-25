namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioApellidos
    {
        public string Value { get; }
        
        public UsuarioApellidos(string apelidos)
        {
            Value = apelidos;
        }
    }
}