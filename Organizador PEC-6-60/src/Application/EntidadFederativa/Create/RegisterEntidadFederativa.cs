using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Create;

public class RegisterEntidadFederativa
{
    private readonly EntidadFederativaCreatorService _creator;

    public RegisterEntidadFederativa(EntidadFederativaCreatorService creator)
    {
        _creator = creator;
    }

    public void Register(int clave, string nombre)
    {
        _creator.Create(new EntidadFederativaClave(clave), new EntidadFederativaNombre(nombre));
    }
}