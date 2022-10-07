using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Municipio.Application.Update;

public interface MunicipioUpdaterService
{
    public void Update(int id, MunicipioClave clave, MunicipioNombre nombre,
        EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa
    );
}