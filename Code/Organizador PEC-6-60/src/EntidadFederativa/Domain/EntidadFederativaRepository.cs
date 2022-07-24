using System.Collections.ObjectModel;

namespace Organizador_PEC_6_60.EntidadFederativa.Domain
{
    public interface EntidadFederativaRepository
    {
        Result<ObservableCollection<EntidadFederativa>> FindAll();
        Result<EntidadFederativa> FindById(int id);
        Result<string> Insert(EntidadFederativa newEntidadFederativa);
        Result<string> Update(EntidadFederativa entidadFederativa);
        Result<string> Delete(EntidadFederativa entidadFederativa);
    }
}

