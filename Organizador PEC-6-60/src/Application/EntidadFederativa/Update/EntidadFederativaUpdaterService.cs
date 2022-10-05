using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;

namespace Organizador_PEC_6_60.Application.EntidadFederativa.Update;

public interface EntidadFederativaUpdaterService
{
    public void Update(int id, EntidadFederativaClave clave, EntidadFederativaNombre nombre);
}