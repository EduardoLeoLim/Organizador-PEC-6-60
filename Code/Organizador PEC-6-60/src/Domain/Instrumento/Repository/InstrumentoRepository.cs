using System.Collections.Generic;

namespace Organizador_PEC_6_60.Domain.Instrumento.Repository
{
    public interface InstrumentoRepository
    {
        IEnumerable<Domain.Instrumento.Model.Instrumento> SearchByCriteria(Dictionary<string, object> dictionary);
        Domain.Instrumento.Model.Instrumento SearchById(int id);
        void Insert(Domain.Instrumento.Model.Instrumento newPec660);
        void Update(Domain.Instrumento.Model.Instrumento pec660);
        void Delete(int id);
    }
}