namespace Organizador_PEC_6_60.Domain.Usuario.ValueObjects
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