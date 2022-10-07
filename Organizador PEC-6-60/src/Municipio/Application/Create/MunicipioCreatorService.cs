using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Create;

public interface MunicipioCreatorService
{
    public void Create(MunicipioClave clave, MunicipioNombre nombre,
        EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa);
}