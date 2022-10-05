using System;

namespace Organizador_PEC_6_60.Domain.Municipio.Exceptions;

public class InvalidClaveMunicipio : Exception
{
    public InvalidClaveMunicipio() : base("La clave del munucipio es inválida.")
    {
    }
}