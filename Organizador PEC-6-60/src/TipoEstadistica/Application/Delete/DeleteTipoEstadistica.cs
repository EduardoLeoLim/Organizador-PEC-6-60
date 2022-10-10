namespace Organizador_PEC_6_60.TipoEstadistica.Application.Delete;

public class DeleteTipoEstadistica
{
    private readonly TipoEstadisticaDeleterService _deleter;

    public DeleteTipoEstadistica(TipoEstadisticaDeleterService deleter)
    {
        _deleter = deleter;
    }

    public void Delete(int id)
    {
        _deleter.Delete(id);
    }

}