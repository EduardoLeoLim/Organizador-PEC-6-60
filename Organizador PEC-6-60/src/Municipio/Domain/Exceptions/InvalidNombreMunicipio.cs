using System;

namespace Organizador_PEC_6_60.Municipio.Domain.Exceptions;

public class InvalidNombreMunicipio : Exception
{
    public InvalidNombreMunicipio() : base("La clave del municipio es inválida.")
    {
    }
}