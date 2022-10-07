using System.Collections.Generic;

namespace Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;

public interface TipoInstrumentoRepository
{
    IEnumerable<Model.TipoInstrumento> SearchAll();
    Model.TipoInstrumento SearchById(int id);
    void Insert(Model.TipoInstrumento newTipoInstrumento);
    void Update(Model.TipoInstrumento tipoInstrumento);
    void Delete(int id);
}