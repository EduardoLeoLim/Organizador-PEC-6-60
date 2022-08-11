using System;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Exceptions
{
    public class InvalidNombreTipoEstadistica : Exception
    {
        public InvalidNombreTipoEstadistica() : base("El nombre del tipo de estadística no es válido.") {}
    }
}