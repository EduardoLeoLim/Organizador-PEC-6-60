using System.Collections.Generic;

namespace Organizador_PEC_6_60.Instrumento.Domain.Repository;

public interface InstrumentoRepository
{
    IEnumerable<Model.Instrumento> SearchAll();
    Model.Instrumento SearchById(int id);
    void Insert(Model.Instrumento newInstrumento);
    void Update(Model.Instrumento instrumento);
    void Delete(int id);
}