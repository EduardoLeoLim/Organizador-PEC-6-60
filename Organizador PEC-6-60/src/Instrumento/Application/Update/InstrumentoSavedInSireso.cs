using Organizador_PEC_6_60.Instrumento.Domain.Repository;

namespace Organizador_PEC_6_60.Instrumento.Application.Update;

public class InstrumentoSavedInSireso
{
    private readonly InstrumentoRepository _repository;

    public InstrumentoSavedInSireso(InstrumentoRepository repository)
    {
        _repository = repository;
    }

    public void SavedInSIRESO(int id)
    {
        var pec660 = _repository.SearchById(id);
        if (!pec660.EstaGuardado)
        {
            pec660.MarcarGuardado();
            _repository.Update(pec660);
        }
    }

    public void UnsavedInSIRESO(int id)
    {
        var pec660 = _repository.SearchById(id);
        if (pec660.EstaGuardado)
        {
            pec660.MarcarNoGuardado();
            _repository.Update(pec660);
        }
    }
}