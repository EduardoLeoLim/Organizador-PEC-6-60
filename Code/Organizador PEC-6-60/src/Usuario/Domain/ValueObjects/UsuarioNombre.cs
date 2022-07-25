namespace Organizador_PEC_6_60.Usuario.Domain.ValueObjects
{
    public class UsuarioNombre
    {
        public string Value { get; }
        
        public UsuarioNombre(string nombre)
        {
            Value = nombre;
        }
    }
}