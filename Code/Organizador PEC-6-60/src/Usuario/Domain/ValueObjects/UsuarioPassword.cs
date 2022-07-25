namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioPassword
    {
        public string Password { get; } 
        
        public UsuarioPassword(string password)
        {
            Password = password;
        }
    }
}