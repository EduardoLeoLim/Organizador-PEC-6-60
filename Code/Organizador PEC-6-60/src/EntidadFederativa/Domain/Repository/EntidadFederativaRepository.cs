using System.Collections.Generic;

namespace Organizador_PEC_6_60.EntidadFederativa.Domain.Repository
{
    public interface EntidadFederativaRepository
    {
        IEnumerable<Model.EntidadFederativa> SearchAll();
        Model.EntidadFederativa SeacrhById(int id);
        void Insert(Model.EntidadFederativa newEntidadFederativa);
        void Update(Model.EntidadFederativa entidadFederativa);
        void Delete(int id);
    }
}