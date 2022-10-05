﻿using System.Collections.Generic;

namespace Organizador_PEC_6_60.Application.Instrumento;

public class InstrumentosData
{
    public InstrumentosData(IEnumerable<InstrumentoData> instrumentos)
    {
        Instrumentos = instrumentos;
    }

    public IEnumerable<InstrumentoData> Instrumentos { get; }
}