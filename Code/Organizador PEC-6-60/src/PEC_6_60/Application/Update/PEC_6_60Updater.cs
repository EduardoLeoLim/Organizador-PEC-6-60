using Organizador_PEC_6_60.PEC_6_60.Domain.Exceptions;
using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;
using Organizador_PEC_6_60.PEC_6_60.Domain.ValueObjects;

namespace Organizador_PEC_6_60.PEC_6_60.Application.Update
{
    public class PEC_6_60Updater
    {
        private readonly PEC_6_60Repository _repository;

        public PEC_6_60Updater(PEC_6_60Repository repository)
        {
            _repository = repository;
        }

        public void Update(int id, PEC_6_60AñoEstadistico añoEstadistico, PEC_6_60MesEstadistico mesEstadistico,
            PEC_6_60Consecutivo consecutivo, byte[] dataArchivo, int idInstrumento, int idTipoEstadistica,
            int idMunicipio)
        {
            if (!IsValid(añoEstadistico))
                throw new InvalidAñoEstadisticoPEC_6_60();
            if (!IsValid(mesEstadistico))
                throw new InvalidMesEstadisticoPEC_6_60();
            if (IsValid(consecutivo))
                throw new InvalidConsecutivoPEC_6_60();
            
            Domain.Model.PEC_6_60 pec660 = new(añoEstadistico, mesEstadistico, consecutivo, dataArchivo, idInstrumento,
                idTipoEstadistica, idMunicipio, id: id);
            _repository.Update(pec660);
        }

        private bool IsValid(object obj)
        {
            if (obj is PEC_6_60AñoEstadistico)
            {
                string year = ((PEC_6_60AñoEstadistico)obj).Value;
                if (string.IsNullOrEmpty(year))
                    return false;
                if (year.Trim().Length == 0)
                    return false;

                return true;
            }

            if (obj is PEC_6_60MesEstadistico)
            {
                int indexMonth = ((PEC_6_60MesEstadistico)obj).Value;
                if (!(indexMonth >= 1 && indexMonth <= 12))
                    return false;

                return true;
            }

            if (obj is PEC_6_60Consecutivo)
            {
                int consecutivo = ((PEC_6_60Consecutivo)obj).Value;
                if (!(consecutivo > 0))
                    return false;

                return true;
            }

            return false;
        }
    }
}