using Organizador_PEC_6_60.Application.EntidadFederativa;
using Organizador_PEC_6_60.Instrumento.Application;
using Organizador_PEC_6_60.Municipio.Application;
using Organizador_PEC_6_60.TipoEstadistica.Application;

namespace Organizador_PEC_6_60.PEC_6_60.Application
{
    public class PEC_6_60Response
    {
        public int Id { get; }
        public string FechaRegistro { get; }
        public string FechaModificacion { get; }
        public string AñoEstadistico { get; }
        public MesEstadistico MesEstadistico { get; }
        public bool GuardadoSIRESO { get; }
        public int Consecutivo { get; }
        public byte[] Archivo { get; }
        public InstrumentoResponse Instrumento { get; }
        public TipoEstadisticaResponse TipoEstadistica { get; }
        public EntidadFederativaResponse EntidadFederativa { get; }
        public MunicipioResponse Municipio { get; }

        public string Nombre => $"{TipoEstadistica.Clave:000}{EntidadFederativa.Clave:00}{AñoEstadistico.Substring(2, 2)}_{Municipio.Clave:000}-{Consecutivo:0000}_{MesEstadistico.Id:00}";

        public PEC_6_60Response(int id, string fechaRegistro, string fechaModificacion, string añoEstadistico,
            int mesEstadistico, bool estaGuardado, int consecutivo, byte[] archivo, InstrumentoResponse instrumento,
            TipoEstadisticaResponse tipoEstadistica, EntidadFederativaResponse entidadFederativa,
            MunicipioResponse municipio)
        {
            Id = id;
            FechaRegistro = fechaRegistro;
            FechaModificacion = fechaModificacion;
            AñoEstadistico = añoEstadistico;
            MesEstadistico = new MesEstadistico(mesEstadistico);
            GuardadoSIRESO = estaGuardado;
            Consecutivo = consecutivo;
            Archivo = archivo;
            Instrumento = instrumento;
            TipoEstadistica = tipoEstadistica;
            EntidadFederativa = entidadFederativa;
            Municipio = municipio;
        }

        public static PEC_6_60Response FromAggregate(Domain.Model.PEC_6_60 PEC_6_60,
            TipoEstadistica.Domain.Model.TipoEstadistica tipoEstadistica,
            Instrumento.Domain.Model.Instrumento instrumento,
            Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa,
            Municipio.Domain.Model.Municipio municipio)
        {
            return new PEC_6_60Response(PEC_6_60.Id, PEC_6_60.FechaModificacion, PEC_6_60.FechaModificacion,
                PEC_6_60.AñoEstadistico.Value, PEC_6_60.MesEstadistico.Value, PEC_6_60.EstaGuardado,
                PEC_6_60.Consecutivo.Value, PEC_6_60.Archivo, InstrumentoResponse.FromAggregate(instrumento),
                TipoEstadisticaResponse.FromAggregate(tipoEstadistica),
                EntidadFederativaResponse.FromAggregate(entidadFederativa),
                MunicipioResponse.FromAggregate(municipio, entidadFederativa));
        }
    }
}