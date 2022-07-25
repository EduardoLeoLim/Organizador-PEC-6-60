namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioApellidos
    {
        public string Apellidos { get; }
        
        public UsuarioApellidos(string apelidos)
        {
            Apellidos = apelidos;
        }
    }
}