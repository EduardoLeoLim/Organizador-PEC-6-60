namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public interface EntidadFederativaByIdSearcherService
    {
        public Domain.EntidadFederativa.Model.EntidadFederativa SearchById(int idEntidadFederetiva);
    }
}