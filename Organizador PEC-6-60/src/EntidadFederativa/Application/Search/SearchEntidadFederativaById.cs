namespace Organizador_PEC_6_60.EntidadFederativa.Application.Search;

public class SearchEntidadFederativaById
{
    private readonly EntidadFederativaByIdSearcherService _byIdSearcher;

    public SearchEntidadFederativaById(EntidadFederativaByIdSearcherService byIdSearcher)
    {
        _byIdSearcher = byIdSearcher;
    }

    public DataEntidadFederativa SearchById(int idEntidadFederativa)
    {
        return DataEntidadFederativa.FromAggregate(_byIdSearcher.SearchById(idEntidadFederativa));
    }
}