namespace Organizador_PEC_6_60.Application.Municipio.Delete;

public class DeleteMunicipio
{
    private readonly MunicipioDeleterService _deleter;

    public DeleteMunicipio(MunicipioDeleterService deleter)
    {
        _deleter = deleter;
    }

    public void Delete(int idMunicipio)
    {
        _deleter.Delete(idMunicipio);
    }
}