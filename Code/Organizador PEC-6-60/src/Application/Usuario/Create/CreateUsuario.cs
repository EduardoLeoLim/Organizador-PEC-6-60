using Organizador_PEC_6_60.Domain.Usuario.Repository;
using Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

namespace Organizador_PEC_6_60.Application.Usuario.Create;

public class CreateUsuario
{
    private UsuarioCreator _creator;

    public CreateUsuario(UsuarioRepository repository)
    {
        _creator = new UsuarioCreator(repository);
    }

    public void RegisterUsuario(string username, string password, string nombre, string apellidos)
    {
        _creator.Create(
            new UsuarioUsername(username),
            new UsuarioPassword(password),
            new UsuarioNombre(nombre),
            new UsuarioApellidos(apellidos)
        );
    }
}