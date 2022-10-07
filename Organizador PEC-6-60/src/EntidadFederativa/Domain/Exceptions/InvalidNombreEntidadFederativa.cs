using System;

namespace Organizador_PEC_6_60.EntidadFederativa.Domain.Exceptions;

public class InvalidNombreEntidadFederativa : Exception
{
    public InvalidNombreEntidadFederativa() : base("El Nombre de la entidad federativa es inválido.")
    {
    }
}