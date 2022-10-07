namespace Organizador_PEC_6_60.EntidadFederativa.Application.Delete;

public class DeleteEntidadFederativa
{
    private readonly EntidadFederativaDeleterService _deleter;

    public DeleteEntidadFederativa(EntidadFederativaDeleterService deleter)
    {
        _deleter = deleter;
    }

    public void Delete(int idEntidadFederativa)
    {
        _deleter.Delete(idEntidadFederativa);
    }
}