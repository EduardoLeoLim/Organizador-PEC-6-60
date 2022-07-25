using Organizador_PEC_6_60.Usuario.Domain;
using Organizador_PEC_6_60.Usuario.Domain.Repository;
using Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Usuario.Application.Create;

public class CreateUsuario
{
    private UsuarioCreator _creator;

    public CreateUsuario(UsuarioRepository repository)
    {
        _creator = new UsuarioCreator(repository);
    }

    public void RegisterUsuario(string username, string password, string nombre, string apellidos)
    {
        _creator.Create(new UsuarioUsername(username), new UsuarioPassword(password), new UsuarioNombre(nombre),
            new UsuarioApellidos(apellidos));
    }
}