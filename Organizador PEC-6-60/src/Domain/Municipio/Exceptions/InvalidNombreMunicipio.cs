using System;

namespace Organizador_PEC_6_60.Domain.Municipio.Exceptions;

public class InvalidNombreMunicipio : Exception
{
    public InvalidNombreMunicipio() : base("La clave del municipio es inválida.")
    {
    }
}