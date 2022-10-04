namespace Organizador_PEC_6_60.Application.EntidadFederativa.Search
{
    public class SearchAllEntidadesFederativas
    {
        private readonly EntidadFederativaAllSearcherService _searcher;

        public SearchAllEntidadesFederativas(EntidadFederativaAllSearcherService searcher)
        {
            _searcher = searcher;
        }

        public DataEntidadesFederativas SearchAll()
        {
            return new DataEntidadesFederativas(_searcher.SearchAll());
        }
    }
}