using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Update;

public interface EntidadFederativaUpdaterService
{
    public void Update(int id, EntidadFederativaClave clave, EntidadFederativaNombre nombre);
}