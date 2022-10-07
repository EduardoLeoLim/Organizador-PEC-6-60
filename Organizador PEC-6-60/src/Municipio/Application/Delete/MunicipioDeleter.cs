using Organizador_PEC_6_60.Municipio.Domain.Repository;

namespace Organizador_PEC_6_60.Municipio.Application.Delete;

public class MunicipioDeleter : MunicipioDeleterService
{
    private readonly MunicipioRepository _repository;

    public MunicipioDeleter(MunicipioRepository repository)
    {
        _repository = repository;
    }

    public void Delete(int idMunicipio)
    {
        _repository.Delete(idMunicipio);
    }
}