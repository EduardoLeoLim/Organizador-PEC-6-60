using Organizador_PEC_6_60.EntidadFederativa.Application.Search;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Create;

public class RegisterMunicipio
{
    private readonly MunicipioCreatorService _creator;

    public RegisterMunicipio(MunicipioCreatorService creator)
    {
        _creator = creator;
    }

    public void Register(int clave, string nombre, DataEntidadFederativa dataEntidadFederativa)
    {
        var entidadFederativa = new EntidadFederativa.Domain.Model.EntidadFederativa(
            new EntidadFederativaClave(dataEntidadFederativa.Clave),
            new EntidadFederativaNombre(dataEntidadFederativa.Nombre),
            dataEntidadFederativa.Id
        );

        _creator.Create(new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
    }
}