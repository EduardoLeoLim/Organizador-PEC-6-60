using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Create;

public interface EntidadFederativaCreatorService
{
    public void Create(EntidadFederativaClave clave, EntidadFederativaNombre nombre);
}