using Organizador_PEC_6_60.Domain.Instrumento.Exceptions;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.Instrumento.Create
{
    public class InstrumentoCreator
    {
        private readonly InstrumentoRepository _repository;

        public InstrumentoCreator(InstrumentoRepository repository)
        {
            _repository = repository;
        }

        public void Create(InstrumentoAñoEstadistico añoEstadistico, InstrumentoMesEstadistico mesEstadistico,
            InstrumentoConsecutivo consecutivo, byte[] dataArchivo, int idInstrumento, int idTipoEstadistica,
            int idMunicipio)
        {
            if (!IsValid(añoEstadistico))
                throw new InvalidAñoEstadisticoInareumento();
            if (!IsValid(mesEstadistico))
                throw new InvalidMesEstadisticoInstrumento();
            if (!IsValid(consecutivo))
                throw new InvalidConsecutivoInstrumento();

            Domain.Instrumento.Model.Instrumento pec660 = new(añoEstadistico, mesEstadistico,
                consecutivo, dataArchivo, idInstrumento,
                idTipoEstadistica, idMunicipio);
            _repository.Insert(pec660);
        }

        private bool IsValid(object obj)
        {
            if (obj is InstrumentoAñoEstadistico)
            {
                string year = ((InstrumentoAñoEstadistico)obj).Value;
                if (string.IsNullOrEmpty(year))
                    return false;
                if (year.Trim().Length == 0)
                    return false;

                return true;
            }

            if (obj is InstrumentoMesEstadistico)
            {
                int indexMonth = ((InstrumentoMesEstadistico)obj).Value;
                if (!(indexMonth >= 1 && indexMonth <= 12))
                    return false;

                return true;
            }

            if (obj is InstrumentoConsecutivo)
            {
                int consecutivo = ((InstrumentoConsecutivo)obj).Value;
                if (!(consecutivo > 0))
                    return false;

                return true;
            }

            return false;
        }
    }
}