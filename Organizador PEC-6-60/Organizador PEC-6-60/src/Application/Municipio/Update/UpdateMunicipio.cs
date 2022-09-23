using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Update
{
    public class UpdateMunicipio
    {
        private readonly MunicipioUpdaterService _updater;

        public UpdateMunicipio(MunicipioUpdaterService updater)
        {
            _updater = updater;
        }

        public void Update(int id, int clave, string nombre, DataEntidadFederativa dataEntidadFederativa)
        {
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa =
                new Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa(
                    new EntidadFederativaClave(dataEntidadFederativa.Clave),
                    new EntidadFederativaNombre(dataEntidadFederativa.Nombre),
                    dataEntidadFederativa.Id
                );

            _updater.Update(id, new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
        }
    }
}