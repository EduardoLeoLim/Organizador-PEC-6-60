using System.Linq;
using System.Threading;

namespace Organizador_PEC_6_60.PEC_6_60.Application
{
    public class MesEstadistico
    {
        public int Id { get; }
        public string Nombre { get; }

        public MesEstadistico(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public MesEstadistico(int id)
        {
            Id = id;
            if (Enumerable.Range(1, 12).Contains(Id))
                Nombre = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(Id).ToUpper();
            else
                Nombre = "";
        }

        public override string ToString()
        {
            return $"{Id:00} - {Nombre}";
        }
    }
}