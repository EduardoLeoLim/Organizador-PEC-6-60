using Organizador_PEC_6_60.Instrumento.Domain.Exceptions;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Instrumento.Application.Update;

public class InstrumentoUpdater
{
    private readonly InstrumentoRepository _repository;

    public InstrumentoUpdater(InstrumentoRepository repository)
    {
        _repository = repository;
    }

    public void Update(int id, InstrumentoNombre nombre)
    {
        if (IsValid(nombre))
            throw new InvalidNombreInstrumento();

        _repository.Update(new Domain.Model.Instrumento(nombre, id));
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