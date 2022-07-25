namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioNombre
    {
        public string Nombre { get; }
        
        public UsuarioNombre(string nombre)
        {
            Nombre = nombre;
        }
    }
}