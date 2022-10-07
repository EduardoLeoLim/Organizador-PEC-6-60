using System;

namespace Organizador_PEC_6_60.Municipio.Domain.Exceptions;

public class InvalidClaveMunicipio : Exception
{
    public InvalidClaveMunicipio() : base("La clave del munucipio es inválida.")
    {
    }
}