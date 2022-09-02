using Organizador_PEC_6_60.Domain.Instrumento.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;

namespace Organizador_PEC_6_60.Application.Instrumento.Create
{
    public class CreateInstrumento
    {
        private readonly InstrumentoCreator _creator;

        public CreateInstrumento(InstrumentoRepository repository)
        {
            _creator = new InstrumentoCreator(repository);
        }

        public void CreateNewInstrumento(
            int idTipoEstadistica,
            int idInstrumento,
            int idMunicipio,
            string añoEstadistico,
            int mesEstadistico,
            int consecutivo,
            byte[] dataArchivo
        )
        {
            _creator.Create(
                new InstrumentoAñoEstadistico(añoEstadistico),
                new InstrumentoMesEstadistico(mesEstadistico),
                new InstrumentoConsecutivo(consecutivo),
                dataArchivo,
                idInstrumento,
                idTipoEstadistica,
                idMunicipio
            );
        }
    }
}