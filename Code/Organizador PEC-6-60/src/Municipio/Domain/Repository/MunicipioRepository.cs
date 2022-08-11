using System.Collections.Generic;

namespace Organizador_PEC_6_60.Municipio.Domain.Repository
{
    public interface MunicipioRepository
    {
        IEnumerable<Model.Municipio> SearchAll(int idMunicipio);
        Model.Municipio SearchById(int id);
        void Insert(Model.Municipio newMunicipio);
        void Update(Model.Municipio municipio);
        void Delete(int id);
    }
}