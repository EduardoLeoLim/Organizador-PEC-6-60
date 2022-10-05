using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Create;

public interface MunicipioCreatorService
{
    public void Create(MunicipioClave clave, MunicipioNombre nombre,
        Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa);
}