using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Update;

public class UpdateMunicipio
{
    private readonly MunicipioUpdaterService _updater;

    public UpdateMunicipio(MunicipioUpdaterService updater)
    {
        _updater = updater;
    }

    public void Update(int id, int clave, string nombre, DataEntidadFederativa dataEntidadFederativa)
    {
        var entidadFederativa = new EntidadFederativa.Domain.Model.EntidadFederativa(
            new EntidadFederativaClave(dataEntidadFederativa.Clave),
            new EntidadFederativaNombre(dataEntidadFederativa.Nombre),
            dataEntidadFederativa.Id
        );

        _updater.Update(id, new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
    }
}