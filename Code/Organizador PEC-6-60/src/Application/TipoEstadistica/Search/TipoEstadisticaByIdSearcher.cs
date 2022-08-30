using Organizador_PEC_6_60.Domain.TipoEstadistica.Repository;

namespace Organizador_PEC_6_60.Application.TipoEstadistica.Search
{
    public class TipoEstadisticaByIdSearcher
    {
        private readonly TipoEstadisticaRepository _repository;

        public TipoEstadisticaByIdSearcher(TipoEstadisticaRepository repository)
        {
            _repository = repository;
        }

        public Domain.TipoEstadistica.Model.TipoEstadistica SearchTipoEstadisticaById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}