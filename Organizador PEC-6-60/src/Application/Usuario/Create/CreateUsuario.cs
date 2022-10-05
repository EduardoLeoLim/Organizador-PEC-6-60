using Organizador_PEC_6_60.Domain.Usuario.Exceptions;
using Organizador_PEC_6_60.Domain.Usuario.Repository;
using Organizador_PEC_6_60.Domain.Usuario.ValueObjects;

namespace Organizador_PEC_6_60.Application.Usuario.Create;

public class CreateUsuario
{
    private readonly UsuarioCreator _creator;

    public CreateUsuario(UsuarioRepository repository)
    {
        _creator = new UsuarioCreator(repository);
    }

    public void RegisterUsuario(string username, string password, string confirmPassword, string nombre,
        string apellidos)
    {
        if (password != confirmPassword)
            throw new InvalidPassword("Las contraseñas no coinciden.");

        _creator.Create(
            new UsuarioUsername(username),
            new UsuarioPassword(password),
            new UsuarioNombre(nombre),
            new UsuarioApellidos(apellidos)
        );
    }
}