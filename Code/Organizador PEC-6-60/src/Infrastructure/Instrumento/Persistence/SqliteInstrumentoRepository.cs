using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Domain.Instrumento.Repository;
using Organizador_PEC_6_60.Domain.Instrumento.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Infrastructure.Instrumento.Persistence
{
    public class SqliteInstrumentoRepository : InstrumentoRepository
    {
        public IEnumerable<Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento> SearchByCriteria(Dictionary<string, object> dictionary)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                List<string> filters = new List<string>();

                if (dictionary.ContainsKey("@IdTipoEstadistica"))
                    filters.Add("idTipoEstadistica = @IdTipoEstadistica");
                if (dictionary.ContainsKey("@IdInstrumento"))
                    filters.Add("idInstrumento = @IdInstrumento");
                if (dictionary.ContainsKey("@AñoEstadistico"))
                    filters.Add("añoEstadistico = @AñoEstadistico");
                if (dictionary.ContainsKey("@MesEstadistico"))
                    filters.Add("mesEstadistico = @MesEstadistico");
                if (dictionary.ContainsKey("@IdEntidadFederativa"))
                    filters.Add("idEntidadFederativa = @IdEntidadFederativa");
                if (dictionary.ContainsKey("@IdMunicipio"))
                    filters.Add("idMunicipio = @IdMunicipio");
                if (dictionary.ContainsKey("@Consecutivo"))
                    filters.Add("consecutivo = @Consecutivo");
                if (dictionary.ContainsKey("@Guardado"))
                    filters.Add("guardado = @Guardado");

                string filtersQuery = "";
                if (filters.Count > 0)
                    filtersQuery = $"WHERE {string.Join(" AND ", filters)}";
                string query = $"SELECT * FROM pec_6_60_consultas {filtersQuery};";
                DynamicParameters parameters = new DynamicParameters(dictionary);
                var result = connection.Query(query, parameters);

                return result.Select(item => new Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento(
                    new InstrumentoAñoEstadistico((string)item.añoEstadistico),
                    new InstrumentoMesEstadistico((int)item.mesEstadistico),
                    new InstrumentoConsecutivo((int)item.consecutivo),
                    (byte[])item.archivo,
                    (int)item.idInstrumento,
                    (int)item.idTipoEstadistica,
                    (int)item.idMunicipio,
                    (int)item.guardado == 1,
                    (string)item.fechaRegistro,
                    (string)item.fechaModificacion,
                    (int)item.id)
                );
            }
        }

        public Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM pec_6_60 WHERE id = @Id;";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32);

                var result = connection.QuerySingle(query, parameters);
                return new Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento(new InstrumentoAñoEstadistico((string)result.añoEstadistico),
                    new InstrumentoMesEstadistico((int)result.mesEstadistico),
                    new InstrumentoConsecutivo((int)result.consecutivo), (byte[])result.archivo, (int)result.idInstrumento,
                    (int)result.idTipoEstadistica, (int)result.idMunicipio, estaGuardado: (int)result.guardado == 1,
                    (string)result.fechaRegistro, (string)result.fechaModificacion, (int)result.id);
            }
        }

        public void Insert(Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento newPec660)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query =
                    "INSERT INTO pec_6_60 (fechaRegistro, fechaModificacion, añoEstadistico, mesEstadistico, " +
                    "guardado, consecutivo, archivo, idTipoEstadistica, idInstrumento, idMunicipio) " +
                    "VALUES (date('now'),'', @AñoEstadistico, @MesEstadistico, 0, @Consecutivo, @Archivo, " +
                    "@IdTipoEstadistica, @IdInstrumento, @IdMunicipio);";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AñoEstadistico", newPec660.AñoEstadistico.Value, DbType.String);
                parameters.Add("@MesEstadistico", newPec660.MesEstadistico.Value, DbType.Int32);
                parameters.Add("@Consecutivo", newPec660.Consecutivo.Value, DbType.Int32);
                parameters.Add("@Archivo", newPec660.Archivo, DbType.Binary);
                parameters.Add("@IdTipoEstadistica", newPec660.IdTipoEstadistica, DbType.Int32);
                parameters.Add("@IdInstrumento", newPec660.IdInstrumento, DbType.Int32);
                parameters.Add("@IdMunicipio", newPec660.IdMunicipio, DbType.Int32);

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar el PEC-6-60, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una PEC-6-60 registrado con los datos seleccionados.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Update(Organizador_PEC_6_60.Domain.Instrumento.Model.Instrumento pec660)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "UPDATE pec_6_60 " +
                               "SET fechaModificacion = DATE('now'), añoEstadistico = @AñoEstadistico, " +
                               "mesEstadistico = @MesEstadistico, guardado = @Guardado, consecutivo = @Consecutivo, " +
                               "archivo = @Archivo, idInstrumento = @IdInstrumento, idTipoEstadistica = @IdTipoEstadistica, " +
                               "idMunicipio = @IdMunicipio " +
                               "WHERE id = @Id;";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AñoEstadistico", pec660.AñoEstadistico.Value, DbType.String);
                parameters.Add("@MesEstadistico", pec660.MesEstadistico.Value, DbType.Int32);
                parameters.Add("@Guardado", pec660.EstaGuardado ? 1 : 0, DbType.Int32);
                parameters.Add("@Consecutivo", pec660.Consecutivo.Value, DbType.Int32);
                parameters.Add("@Archivo", pec660.Archivo, DbType.Binary);
                parameters.Add("@IdInstrumento", pec660.IdInstrumento, DbType.Int32);
                parameters.Add("@IdTipoEstadistica", pec660.IdTipoEstadistica, DbType.Int32);
                parameters.Add("@IdMunicipio", pec660.IdMunicipio, DbType.Int32);
                parameters.Add("@Id", pec660.Id, DbType.Int32);

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar el PEC-6-60, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una PEC-6-60 registrado con los datos seleccionados.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}