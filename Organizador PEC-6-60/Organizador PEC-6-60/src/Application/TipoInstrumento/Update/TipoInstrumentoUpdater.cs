using Organizador_PEC_6_60.Domain.TipoInstrumento.Exceptions;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoInstrumento.Update
{
    public class TipoInstrumentoUpdater
    {
        private readonly TipoInstrumentoRepository _repository;

        public TipoInstrumentoUpdater(TipoInstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void Update(int id, TipoInstrumentoNombre nombre)
        {
            if (!IsValid(nombre))
                throw new InvalidNombreTipoInstrumento();

            _repository.Update(new Domain.TipoInstrumento.Model.TipoInstrumento(nombre, id));
        }

        public bool IsValid(object obj)
        {
            if (obj is TipoInstrumentoNombre)
            {
                string nombre = ((TipoInstrumentoNombre)obj).Value;

                if (string.IsNullOrEmpty(nombre))
                    return false;
                if (nombre.Trim().Length == 0)
                    return false;
                //Add more validations here

                return true;
            }

            return false;
        }
    }
}