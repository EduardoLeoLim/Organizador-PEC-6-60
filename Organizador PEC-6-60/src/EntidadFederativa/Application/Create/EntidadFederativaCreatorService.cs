using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Create;

public interface EntidadFederativaCreatorService
{
    public void Create(EntidadFederativaClave clave, EntidadFederativaNombre nombre);
}