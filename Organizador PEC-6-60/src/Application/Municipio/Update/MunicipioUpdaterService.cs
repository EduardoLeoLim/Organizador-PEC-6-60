using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Update;

public interface MunicipioUpdaterService
{
    public void Update(int id, MunicipioClave clave, MunicipioNombre nombre,
        Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa
    );
}