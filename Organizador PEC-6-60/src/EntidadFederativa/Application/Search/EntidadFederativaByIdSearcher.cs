using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;

namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search;

public class EntidadFederativaByIdSearcher : EntidadFederativaByIdSearcherService
{
    private readonly EntidadFederativaRepository _repository;

    public EntidadFederativaByIdSearcher(EntidadFederativaRepository repository)
    {
        _repository = repository;
    }

    public Domain.Model.EntidadFederativa SearchById(int idEntidadFederetiva)
    {
        return _repository.SeacrhById(idEntidadFederetiva);
    }
}