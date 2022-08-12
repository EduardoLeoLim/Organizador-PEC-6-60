using System;
using Organizador_PEC_6_60.PEC_6_60.Domain.ValueObjects;

namespace Organizador_PEC_6_60.PEC_6_60.Domain.Model
{
    public class PEC_6_60
    {
        public int Id { get; }
        public string FechaRegistro { get; }
        public string FechaModificacion { get; }
        public PEC_6_60AñoEstadistico AñoEstadistico { get; }
        public PEC_6_60MesEstadistico MesEstadistico { get; }
        public bool EstaGuardado { get; private set; }
        public PEC_6_60Consecutivo Consecutivo { get; }
        public byte[] Archivo { get; }
        public int IdInstrumento { get; }
        public int IdTipoEstadistica { get; }
        public int IdMunicipio { get; }

        public PEC_6_60(string fechaRegistro, string fechaModificacion, PEC_6_60AñoEstadistico añoEstadistico,
            PEC_6_60MesEstadistico mesEstadistico, bool estaGuardado, PEC_6_60Consecutivo consecutivo, byte[] archivo,
            int idInstrumento, int idTipoEstadistica, int idMunicipio, int id = 0)
        {
            FechaRegistro = fechaRegistro;
            FechaModificacion = fechaModificacion;
            AñoEstadistico = añoEstadistico;
            MesEstadistico = mesEstadistico;
            EstaGuardado = estaGuardado;
            Consecutivo = consecutivo;
            Archivo = archivo;
            IdInstrumento = idInstrumento;
            IdTipoEstadistica = idTipoEstadistica;
            IdMunicipio = idMunicipio;
            Id = id;
        }

        private void MarcarGuardado()
        {
            EstaGuardado = true;
        }

        private void MarcarNoGuardado()
        {
            EstaGuardado = false;
        }
    }
}