﻿using System;

namespace Organizador_PEC_6_60.EntidadFederativa.Domain.Exceptions;

public class InvalidClaveEntidadFederativa : Exception
{
    public InvalidClaveEntidadFederativa() : base("La Clave de la entidad federativa es inválida.")
    {
    }
}