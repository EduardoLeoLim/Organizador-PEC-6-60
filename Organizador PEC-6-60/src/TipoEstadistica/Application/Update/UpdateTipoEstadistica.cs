using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.TipoEstadistica.Application.Create;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Update;

public class UpdateTipoEstadistica
{
    private readonly TipoEstadisticaUpdaterService _updater;

    public UpdateTipoEstadistica(TipoEstadisticaUpdaterService updater)
    {
        _updater = updater;
    }

    public void Update(
        int id,
        int clave,
        string nombre,
        List<TipoInstrumentoResponse> instrumentos
    )
    {
        var listInstrumentos = instrumentos.Select(item =>
            new TipoInstrumento.Domain.Model.TipoInstrumento(new TipoInstrumentoNombre(item.Nombre), item.Id)
        ).ToList();

        _updater.Update(id, new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
    }
}