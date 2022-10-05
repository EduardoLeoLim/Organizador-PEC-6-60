using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Update;

public class UpdateEntidadFederativa
{
    private readonly EntidadFederativaUpdaterService _updater;

    public UpdateEntidadFederativa(EntidadFederativaUpdaterService updater)
    {
        _updater = updater;
    }

    public void Update(int id, int clave, string nombre)
    {
        _updater.Update(id, new EntidadFederativaClave(clave), new EntidadFederativaNombre(nombre));
    }
}