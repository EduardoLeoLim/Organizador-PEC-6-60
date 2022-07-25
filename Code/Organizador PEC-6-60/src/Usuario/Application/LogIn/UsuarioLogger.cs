using Organizador_PEC_6_60.Usuario.Domain;
using Organizador_PEC_6_60.Usuario.Domain.Repository;

namespace Organizador_PEC_6_60.Usuario.Application.LogIn;

public class UsuarioLogger
{
    private readonly UsuarioRepository _repository;

    public UsuarioLogger(UsuarioRepository repository)
    {
        _repository = repository;
    }

    public UsuarioConnected LogInUsuario(string username, string password)
    {
        Domain.Model.Usuario usuario = _repository.LogIn(username, password);
        return new UsuarioConnected(usuario.Id, usuario.Nombre.Value, usuario.Apellidos.Value, usuario.Username.Value, usuario.Password.Value);
    }
}