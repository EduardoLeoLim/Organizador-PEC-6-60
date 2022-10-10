namespace Organizador_PEC_6_60.TipoEstadistica.Application.Search;

public class SearchAllTiposEstadistica
{
    private readonly AllTipoEstadisticaSearcherService _allSearcher;

    public SearchAllTiposEstadistica(AllTipoEstadisticaSearcherService allSearcher)
    {
        _allSearcher = allSearcher;
    }
    
    public TiposEstadisticaResponse SearchAll()
    {
        return new TiposEstadisticaResponse(_allSearcher.SearchAll());
    }
}