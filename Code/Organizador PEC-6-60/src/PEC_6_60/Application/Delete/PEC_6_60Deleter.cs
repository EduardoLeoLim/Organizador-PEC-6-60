using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Delete
{
    public class PEC_6_60Deleter
    {
        private readonly PEC_6_60Repository _repository;

        public PEC_6_60Deleter(PEC_6_60Repository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}