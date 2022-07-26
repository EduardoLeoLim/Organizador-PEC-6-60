﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Organizador_PEC_6_60.Instrumento.Application;

public class MesEstadistico
{
    public static readonly IEnumerable<MesEstadistico> Meses = Thread.CurrentThread.CurrentCulture.DateTimeFormat
        .MonthNames.Take(12).Select((item, index) => new MesEstadistico(index + 1, item.ToUpper()));

    public MesEstadistico(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }

    public MesEstadistico(int id)
    {
        Id = id;
        if (Enumerable.Range(1, 12).Contains(Id))
            Nombre = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(Id).ToUpper();
        else
            Nombre = "";
    }

    public int Id { get; }
    public string Nombre { get; }

    public override string ToString()
    {
        return $"{Id:00} - {Nombre}";
    }
}