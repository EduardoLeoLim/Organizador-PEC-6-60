using Organizador_PEC_6_60.Application.EntidadFederativa.Search;
using Organizador_PEC_6_60.Application.Municipio.Search;
using Organizador_PEC_6_60.Application.TipoEstadistica;
using Organizador_PEC_6_60.Application.TipoInstrumento;

namespace Organizador_PEC_6_60.Application.Instrumento;

public class InstrumentoData
{
    public InstrumentoData(
        int id,
        string fechaRegistro,
        string fechaModificacion,
        string añoEstadistico,
        int mesEstadistico,
        bool estaGuardado,
        int consecutivo,
        byte[] archivo, TipoInstrumentoResponse tipoInstrumento,
        TipoEstadisticaResponse tipoEstadistica,
        DataEntidadFederativa entidadFederativa,
        DataMunicipio municipio
    )
    {
        Id = id;
        FechaRegistro = fechaRegistro;
        FechaModificacion = fechaModificacion;
        AñoEstadistico = añoEstadistico;
        MesEstadistico = new MesEstadistico(mesEstadistico);
        GuardadoSIRESO = estaGuardado;
        Consecutivo = consecutivo;
        Archivo = archivo;
        TipoInstrumento = tipoInstrumento;
        TipoEstadistica = tipoEstadistica;
        EntidadFederativa = entidadFederativa;
        Municipio = municipio;
    }

    public int Id { get; }
    public string FechaRegistro { get; }
    public string FechaModificacion { get; }
    public string AñoEstadistico { get; }
    public MesEstadistico MesEstadistico { get; }
    public bool GuardadoSIRESO { get; }
    public int Consecutivo { get; }
    public byte[] Archivo { get; }
    public TipoInstrumentoResponse TipoInstrumento { get; }
    public TipoEstadisticaResponse TipoEstadistica { get; }
    public DataEntidadFederativa EntidadFederativa { get; }
    public DataMunicipio Municipio { get; }

    public string Nombre =>
        $"{TipoEstadistica.Clave:000}{EntidadFederativa.Clave:00}{AñoEstadistico.Substring(2, 2)}_{Municipio.Clave:000}-{Consecutivo:0000}_{MesEstadistico.Id:00}";

    public static InstrumentoData FromAggregate(
        Domain.Instrumento.Model.Instrumento instrumento,
        Domain.TipoEstadistica.Model.TipoEstadistica tipoEstadistica,
        Domain.TipoInstrumento.Model.TipoInstrumento tipoInstrumento,
        Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa,
        Domain.Municipio.Model.Municipio municipio
    )
    {
        return new InstrumentoData(
            instrumento.Id,
            instrumento.FechaRegistro,
            instrumento.FechaModificacion,
            instrumento.AñoEstadistico.Value,
            instrumento.MesEstadistico.Value,
            instrumento.EstaGuardado,
            instrumento.Consecutivo.Value,
            instrumento.Archivo,
            TipoInstrumentoResponse.FromAggregate(tipoInstrumento),
            TipoEstadisticaResponse.FromAggregate(tipoEstadistica),
            DataEntidadFederativa.FromAggregate(entidadFederativa),
            DataMunicipio.FromAggregate(municipio, entidadFederativa)
        );
    }
}