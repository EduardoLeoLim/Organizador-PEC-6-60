using Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;

namespace Organizador_PEC_6_60.Domain.Instrumento.Model
{
    public class Instrumento
    {
        public int Id { get; }
        public string FechaRegistro { get; }
        public string FechaModificacion { get; }
        public InstrumentoAñoEstadistico AñoEstadistico { get; }
        public InstrumentoMesEstadistico MesEstadistico { get; }
        public bool EstaGuardado { get; private set; }
        public InstrumentoConsecutivo Consecutivo { get; }
        public byte[] Archivo { get; }
        public int IdInstrumento { get; }
        public int IdTipoEstadistica { get; }
        public int IdMunicipio { get; }

        public Instrumento(InstrumentoAñoEstadistico añoEstadistico, InstrumentoMesEstadistico mesEstadistico,
            InstrumentoConsecutivo consecutivo, byte[] archivo, int idInstrumento, int idTipoEstadistica, int idMunicipio,
            bool estaGuardado = false, string fechaRegistro = "", string fechaModificacion = "", int id = 0)
        {
            AñoEstadistico = añoEstadistico;
            MesEstadistico = mesEstadistico;
            EstaGuardado = estaGuardado;
            Consecutivo = consecutivo;
            Archivo = archivo;
            IdInstrumento = idInstrumento;
            IdTipoEstadistica = idTipoEstadistica;
            IdMunicipio = idMunicipio;
            FechaRegistro = fechaRegistro;
            FechaModificacion = fechaModificacion;
            Id = id;
        }

        public void MarcarGuardado()
        {
            EstaGuardado = true;
        }

        public void MarcarNoGuardado()
        {
            EstaGuardado = false;
        }
    }
}