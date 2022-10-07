using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Create;

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