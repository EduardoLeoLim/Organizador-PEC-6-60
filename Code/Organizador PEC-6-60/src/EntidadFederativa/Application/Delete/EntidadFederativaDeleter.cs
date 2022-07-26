using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Delete;

public class EntidadFederativaDeleter
{
    private readonly EntidadFederativaRepository _repository;

    public EntidadFederativaDeleter(EntidadFederativaRepository repository)
    {
        _repository = repository;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}