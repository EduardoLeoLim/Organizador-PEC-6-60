using System.Collections.Generic;

namespace Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;

public interface TipoEstadisticaRepository
{
    IEnumerable<Model.TipoEstadistica> SearchAll();
    Model.TipoEstadistica SearchById(int idTipoEstadistica);
    void Insert(Model.TipoEstadistica newTipoEstadistica);
    void Update(Model.TipoEstadistica tipoEstadistica);
    void Delete(int id);
}