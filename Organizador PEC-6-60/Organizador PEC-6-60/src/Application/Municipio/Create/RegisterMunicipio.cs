using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;

namespace Organizador_PEC_6_60.Application.Municipio.Create
{
    public class RegisterMunicipio
    {
        private readonly MunicipioCreatorService _creator;

        public RegisterMunicipio(MunicipioCreatorService creator)
        {
            _creator = creator;
        }

        public void Register(int clave, string nombre, DataEntidadFederativa dataEntidadFederativa)
        {
            Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa =
                new Domain.EntidadFederativa.Model.EntidadFederativa(
                    new EntidadFederativaClave(dataEntidadFederativa.Clave),
                    new EntidadFederativaNombre(dataEntidadFederativa.Nombre),
                    dataEntidadFederativa.Id
                );

            _creator.Create(new MunicipioClave(clave), new MunicipioNombre(nombre), entidadFederativa);
        }
    }
}