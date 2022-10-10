using System.Collections.Generic;
using System.Linq;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;
using Organizador_PEC_6_60.TipoInstrumento.Application;
using Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Application.Create;

public class RegisterTipoEstadistica
{
    private readonly TipoEstadisticaCreatorService _creator;
    
    public RegisterTipoEstadistica(TipoEstadisticaCreatorService creator)
    {
        _creator = creator;
    }
    
    public void Register(int clave, string nombre, List<TipoInstrumentoResponse> instrumentos)
    {
        var listInstrumentos = instrumentos.Select(item =>
            new TipoInstrumento.Domain.Model.TipoInstrumento(new TipoInstrumentoNombre(item.Nombre), item.Id)
        ).ToList();

        _creator.Create(new TipoEstadisticaClave(clave), new TipoEstadisticaNombre(nombre), listInstrumentos);
    }
}