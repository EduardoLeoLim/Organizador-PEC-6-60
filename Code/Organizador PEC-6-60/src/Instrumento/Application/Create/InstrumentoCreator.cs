using Organizador_PEC_6_60.Instrumento.Domain.Exceptions;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Application.Create
{
    public class InstrumentoCreator
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoCreator(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void Create(InstrumentoNombre nombre)
        {
            if (!IsValid(nombre))
                throw new InvalidNombreInstrumento();
            
            _repository.Insert(new Domain.Model.Instrumento(nombre));
        }

        public bool IsValid(object obj)
        {
            if (obj is InstrumentoNombre)
            {
                string nombre = ((InstrumentoNombre)obj).Value;

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