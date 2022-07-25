namespace Organizador_PEC_6_60.Usuario.Application
{
    public class UsuarioConnected
    {
        public int Id { get; }
        public string Nombre { get; } 
        public string Apellidos { get; } 
        public string Username { get; } 
        public string Password { get; }

        public UsuarioConnected(int id, string nombre, string apellidos, string username, string password)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Username = username;
            Password = password;
        }
    }
}

