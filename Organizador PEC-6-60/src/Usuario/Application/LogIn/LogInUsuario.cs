using Organizador_PEC_6_60.Usuario.Domain.Repository;

namespace Organizador_PEC_6_60.Usuario.Application.LogIn;

public class LogInUsuario
{
    private readonly UsuarioLogger _logger;

    public LogInUsuario(UsuarioRepository repository)
    {
        _logger = new UsuarioLogger(repository);
    }

    public UsuarioConnected LogIn(string username, string password)
    {
        return _logger.LogInUsuario(username, password);
    }
}