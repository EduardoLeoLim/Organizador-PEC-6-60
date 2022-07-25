namespace Organizador_PEC_6_60.Usuario.Domain.Repository
{
    public interface UsuarioRepository
    {
        Model.Usuario FindById(int id);
        Model.Usuario LogIn(string username, string password);
        void Insert(Model.Usuario newUsuario);
        void Update(Model.Usuario usuario);
        void Delete(int id);
    }
}

