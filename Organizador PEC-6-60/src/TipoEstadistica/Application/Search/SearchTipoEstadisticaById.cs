namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class SearchTipoEstadisticaById
{
    private readonly TipoEstadisticaByIdSearcherService _byIdSearcher;

    public SearchTipoEstadisticaById(TipoEstadisticaByIdSearcherService byIdSearcher)
    {
        _byIdSearcher = byIdSearcher;
    }
    
    public TipoEstadisticaResponse SearchById(int id)
    {
        return TipoEstadisticaResponse.FromAggregate(_byIdSearcher.SearchTipoEstadisticaById(id));
    }
}