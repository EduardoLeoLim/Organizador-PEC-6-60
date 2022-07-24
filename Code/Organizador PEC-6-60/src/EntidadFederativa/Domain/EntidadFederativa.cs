namespace Organizador_PEC_6_60.EntidadFederativa.Domain
{
    public class EntidadFederativa
    {
        #region Properties

        public int Id { get; }
        public int Folio { get; }
        public string Nombre { get; }

        #endregion

        #region Constructors

        public EntidadFederativa(int id, int folio, string nombre)
        {
            Id = id;
            Folio = folio;
            Nombre = nombre;
        }

        #endregion
    }   
}