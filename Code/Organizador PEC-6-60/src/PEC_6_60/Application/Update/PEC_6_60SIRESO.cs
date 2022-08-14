using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Update
{
    public class PEC_6_60SIRESO
    {
        private readonly PEC_6_60Repository _repository;

        public PEC_6_60SIRESO(PEC_6_60Repository repository)
        {
            _repository = repository;
        }

        public void SavedInSIRESO(int id)
        {
            Domain.Model.PEC_6_60 pec660 = _repository.SearchById(id);
            pec660.MarcarGuardado();
            _repository.Update(pec660);
        }

        public void UnsavedInSIRESO(int id)
        {
            Domain.Model.PEC_6_60 pec660 = _repository.SearchById(id);
            pec660.MarcarNoGuardado();
            _repository.Update(pec660);
        }
    }
}