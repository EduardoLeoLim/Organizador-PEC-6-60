namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class DataEntidadFederativa
    {
        public int Id { get; }
        public int Clave { get; }
        public string Nombre { get; }

        private DataEntidadFederativa(int id, int clave, string nombre)
        {
            Id = id;
            Clave = clave;
            Nombre = nombre;
        }

        public static DataEntidadFederativa FromAggregate(
            Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            return new DataEntidadFederativa(entidadFederativa.Id, entidadFederativa.Clave.Value,
                entidadFederativa.Nombre.Value);
        }

        public override string ToString()
        {
            return $"{Clave:00} - {Nombre}";
        }
    }
}