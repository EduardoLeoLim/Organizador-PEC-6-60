using Organizador_PEC_6_60.Domain.Usuario.Repository;

namespace Organizador_PEC_6_60.Application.Usuario.LogIn;

public class UsuarioLogger
{
    private readonly UsuarioRepository _repository;

    public UsuarioLogger(UsuarioRepository repository)
    {
        _repository = repository;
    }

    public UsuarioConnected LogInUsuario(string username, string password)
    {
        var usuario = _repository.LogIn(username, password);

        return new UsuarioConnected(
            usuario.Id,
            usuario.Nombre.Value,
            usuario.Apellidos.Value,
            usuario.Username.Value,
            usuario.Password.Value
        );
    }
}