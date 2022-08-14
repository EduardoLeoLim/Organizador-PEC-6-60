using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Search
{
    public class PEC_6_60ByIdSearcher
    {
        private readonly PEC_6_60Repository _repository;

        public PEC_6_60ByIdSearcher(PEC_6_60Repository repository)
        {
            _repository = repository;
        }

        public Domain.Model.PEC_6_60 SearchPEC_6_60ById(int id)
        {
            return _repository.SearchById(id);
        }
    }
}