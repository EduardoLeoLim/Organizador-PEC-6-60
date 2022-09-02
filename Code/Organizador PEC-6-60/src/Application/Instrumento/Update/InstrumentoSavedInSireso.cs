using Organizador_PEC_6_60.Domain.Instrumento.Repository;

namespace Organizador_PEC_6_60.Application.Instrumento.Update
{
    public class InstrumentoSavedInSireso
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoSavedInSireso(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void SavedInSIRESO(int id)
        {
            Domain.Instrumento.Model.Instrumento pec660 = _repository.SearchById(id);
            if (!pec660.EstaGuardado)
            {
                pec660.MarcarGuardado();
                _repository.Update(pec660);
            }
        }

        public void UnsavedInSIRESO(int id)
        {
            Domain.Instrumento.Model.Instrumento pec660 = _repository.SearchById(id);
            if (pec660.EstaGuardado)
            {
                pec660.MarcarNoGuardado();
                _repository.Update(pec660);
            }
        }
    }
}