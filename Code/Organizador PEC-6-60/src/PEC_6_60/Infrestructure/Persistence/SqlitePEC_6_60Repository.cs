using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.PEC_6_60.Domain.Repository;
using Organizador_PEC_6_60.PEC_6_60.Domain.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.PEC_6_60.Infrestructure.Persistence
{
    public class SqlitePEC_6_60Repository : PEC_6_60Repository
    {
        public IEnumerable<Domain.Model.PEC_6_60> SearchByCriteria(Dictionary<string, object> dictionary)
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

                return result.Select(item => new Domain.Model.PEC_6_60(
                    new PEC_6_60AñoEstadistico((string)item.añoEstadistico),
                    new PEC_6_60MesEstadistico((int)item.mesEstadistico),
                    new PEC_6_60Consecutivo((int)item.consecutivo), (byte[])item.archivo, (int)item.idInstrumento,
                    (int)item.idTipoEstadistica, (int)item.idMunicipio, estaGuardado: (int)item.guardado == 1,
                    (string)item.fechaRegistro, (string)item.fechaModificacion, (int)item.id));
            }
        }

        public Domain.Model.PEC_6_60 SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM pec_6_60 WHERE id = @Id;";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int32);

                var result = connection.QuerySingle(query, parameters);
                return new Domain.Model.PEC_6_60(new PEC_6_60AñoEstadistico((string)result.añoEstadistico),
                    new PEC_6_60MesEstadistico((int)result.mesEstadistico),
                    new PEC_6_60Consecutivo((int)result.consecutivo), (byte[])result.archivo, (int)result.idInstrumento,
                    (int)result.idTipoEstadistica, (int)result.idMunicipio, estaGuardado: (int)result.guardado == 1,
                    (string)result.fechaRegistro, (string)result.fechaModificacion, (int)result.id);
            }
        }

        public void Insert(Domain.Model.PEC_6_60 newPec660)
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
                DynamicParameters paramenters = new DynamicParameters();
                paramenters.Add("@AñoEstadistico", newPec660.AñoEstadistico.Value, DbType.String);
                paramenters.Add("@MesEstadistico", newPec660.MesEstadistico.Value, DbType.Int32);
                paramenters.Add("@Consecutivo", newPec660.Consecutivo.Value, DbType.Int32);
                paramenters.Add("@Archivo", newPec660.Archivo, DbType.Binary);
                paramenters.Add("@IdTipoEstadistica", newPec660.IdTipoEstadistica, DbType.Int32);
                paramenters.Add("@IdInstrumento", newPec660.IdInstrumento, DbType.Int32);
                paramenters.Add("@IdMunicipio", newPec660.IdMunicipio, DbType.Int32);

                try
                {
                    int affectedRows = connection.Execute(query, paramenters);

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

        public void Update(Domain.Model.PEC_6_60 pec660)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}