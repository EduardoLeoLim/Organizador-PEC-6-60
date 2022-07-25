using Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Usuario.Domain.Model;

public class Usuario
{
    #region Properties

    public int Id { get; }
    public UsuarioUsername Username { get; }
    public UsuarioPassword Password { get; }
    public UsuarioNombre Nombre { get; }
    public UsuarioApellidos Apellidos { get; }

    #endregion

    #region Constructors

    public Usuario(UsuarioUsername username, UsuarioPassword password, UsuarioNombre nombre,
        UsuarioApellidos apellidos, int id = 0)
    {
        Id = id;
        Username = username;
        Password = password;
        Nombre = nombre;
        Apellidos = apellidos;
    }

    #endregion
}