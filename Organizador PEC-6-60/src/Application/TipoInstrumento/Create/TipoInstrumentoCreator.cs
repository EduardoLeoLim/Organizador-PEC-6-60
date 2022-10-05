using Organizador_PEC_6_60.Domain.TipoInstrumento.Exceptions;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.TipoInstrumento.Create;

public class TipoInstrumentoCreator
{
    private readonly TipoInstrumentoRepository _repository;

    public TipoInstrumentoCreator(TipoInstrumentoRepository repository)
    {
        _repository = repository;
    }

    public void Create(TipoInstrumentoNombre nombre)
    {
        if (!IsValid(nombre))
            throw new InvalidNombreTipoInstrumento();

        _repository.Insert(new Domain.TipoInstrumento.Model.TipoInstrumento(nombre));
    }

    public bool IsValid(object obj)
    {
        if (obj is TipoInstrumentoNombre)
        {
            var nombre = ((TipoInstrumentoNombre)obj).Value;

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