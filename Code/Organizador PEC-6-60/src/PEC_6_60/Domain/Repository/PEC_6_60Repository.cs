using System.Collections.Generic;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.Repository
{
    public interface PEC_6_60Repository
    {
        IEnumerable<Model.PEC_6_60> SearchByCriteria(Dictionary<string, object> dictionary);
        Model.PEC_6_60 SearchById(int id);
        void Insert(Model.PEC_6_60 newPec660);
        void Update(Model.PEC_6_60 pec660);
        void Delete(int id);
    }
}