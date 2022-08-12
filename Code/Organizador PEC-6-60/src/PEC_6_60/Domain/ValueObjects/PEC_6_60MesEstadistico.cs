﻿using System.Globalization;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.ValueObjects;

public class PEC_6_60MesEstadistico
{
    public int Value { get; }

    public PEC_6_60MesEstadistico(int mesEstadistico)
    {
        Value = mesEstadistico;
    }
}