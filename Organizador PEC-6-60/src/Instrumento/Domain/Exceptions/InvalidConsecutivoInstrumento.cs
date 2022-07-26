﻿using System;

namespace Organizador_PEC_6_60.Instrumento.Domain.Exceptions;

public class InvalidConsecutivoInstrumento : Exception
{
    public InvalidConsecutivoInstrumento() : base("El formato del consecutivo no es válido.")
    {
    }

    public InvalidConsecutivoInstrumento(string message) : base(message)
    {
    }
}