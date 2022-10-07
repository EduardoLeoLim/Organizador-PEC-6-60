using System.Collections.Generic;

namespace Organizador_PEC_6_60.Instrumento.Domain.Repository;

public interface InstrumentoRepository
{
    IEnumerable<Model.Instrumento> SearchByCriteria(Dictionary<string, object> dictionary);
    Model.Instrumento SearchById(int id);
    void Insert(Model.Instrumento newPec660);
    void Update(Model.Instrumento pec660);
    void Delete(int id);
}