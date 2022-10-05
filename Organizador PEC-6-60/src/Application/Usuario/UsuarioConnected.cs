namespace Organizador_PEC_6_60.Application.Usuario;

public class UsuarioConnected
{
    public UsuarioConnected(int id, string nombre, string apellidos, string username, string password)
    {
        Id = id;
        Nombre = nombre;
        Apellidos = apellidos;
        Username = username;
        Password = password;
    }

    public int Id { get; }
    public string Nombre { get; }
    public string Apellidos { get; }
    public string Username { get; }
    public string Password { get; }
}