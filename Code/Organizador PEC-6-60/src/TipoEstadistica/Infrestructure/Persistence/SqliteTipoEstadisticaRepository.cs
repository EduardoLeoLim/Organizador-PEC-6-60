﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Infrestructure.Persistence
{
    public class SqliteTipoEstadisticaRepository : TipoEstadisticaRepository
    {
        public IEnumerable<Domain.Model.TipoEstadistica> SearchAll()
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                List<Domain.Model.TipoEstadistica> collection = new List<Domain.Model.TipoEstadistica>();

                string queryTipoEstadistica = "SELECT * FROM tipoEstadistica ORDER BY folio;";
                string queryInstrumentos =
                    "SELECT i.id, i.nombre FROM instrumento AS i INNER JOIN estadistica_instrumento AS ei " +
                    "ON (i.id = ei.idInstrumento) " +
                    "WHERE ei.idEstadistica = @IdTipoEstadistica;";

                var resultTipoEstadistica = connection.Query(queryTipoEstadistica);
                foreach (dynamic item in resultTipoEstadistica)
                {
                    var parameters = new { IdTipoEstadistica = (int)item.id };
                    List<Instrumento.Domain.Model.Instrumento> listInstrumentos = connection
                        .Query(queryInstrumentos, parameters)
                        .Select(row => new Instrumento.Domain.Model.Instrumento(
                            new InstrumentoNombre((string)row.nombre),
                            (int)row.id)
                        ).ToList();

                    Domain.Model.TipoEstadistica tipoEstadistica = new Domain.Model.TipoEstadistica(
                        new TipoEstadisticaClave((int)item.folio),
                        new TipoEstadisticaNombre((string)item.nombre),
                        listInstrumentos,
                        (int)item.id
                    );

                    collection.Add(tipoEstadistica);
                }
                connection.Close();

                return collection;
            }
        }

        public Domain.Model.TipoEstadistica SearchById(int idTipoEstadistica)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string queryTipoEstadistica = "SELECT * FROM tipoEstadistica WHERE id = @Id;";
                var paramentersTipoEstadistica = new { Id = idTipoEstadistica };
                var resultTipoEstadistica = connection.QuerySingle(queryTipoEstadistica, paramentersTipoEstadistica);

                string queryInstrumentos =
                    "SELECT i.id, i.nombre FROM instrumento AS i INNER JOIN estadistica_instrumento AS ei " +
                    "ON (i.id = ei.idInstrumento) " +
                    "WHERE ei.idEstadistica = @IdTipoEstadistica;";
                var paramentersInstrumentos = new { IdTipoEstadistica = (int)resultTipoEstadistica.id };

                var instrumentos = connection.Query(queryInstrumentos, paramentersInstrumentos)
                    .Select(row =>
                        new Instrumento.Domain.Model.Instrumento(
                            new InstrumentoNombre((string)row.nombre),
                            (int)row.id
                        )
                    ).ToList();
                connection.Close();

                Domain.Model.TipoEstadistica tipoEstadistica = new Domain.Model.TipoEstadistica(
                    new TipoEstadisticaClave((int)resultTipoEstadistica.folio),
                    new TipoEstadisticaNombre((string)resultTipoEstadistica.nombre),
                    instrumentos,
                    (int)resultTipoEstadistica.id
                );

                return tipoEstadistica;
            }
        }

        public void Insert(Domain.Model.TipoEstadistica newTipoEstadistica)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Insert TipoEstadistica
                        string queryTipoEstadistica =
                            "INSERT INTO tipoEstadistica (folio, nombre) VALUES (@Clave, @Nombre) RETURNING id;";
                        var parametersTipoEstadistica = new
                        {
                            Clave = newTipoEstadistica.Clave.Value,
                            Nombre = newTipoEstadistica.Nombre.Value
                        };

                        var resultTipoEstadistica = connection.QuerySingle(queryTipoEstadistica,
                            parametersTipoEstadistica,
                            transaction);

                        //Insert list of Instrumento
                        string queryInsertInstrumentos =
                            "INSERT INTO estadistica_instrumento(idEstadistica, idInstrumento) " +
                            "VALUES (@IdTipoEstadistica, @IdInstrumento)";
                        var parametersInsertInstrumento = newTipoEstadistica.Instrumentos.Select(
                            row => new { IdTipoEstadistica = (int)resultTipoEstadistica.id, IdInstrumento = row.Id });
                        connection.Execute(queryInsertInstrumentos, parametersInsertInstrumento, transaction);

                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
        }

        public void Update(Domain.Model.TipoEstadistica tipoEstadistica)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Update TipoEstadistica
                        string query = "UPDATE tipoEstadistica SET folio = @Clave, nombre = @Nombre WHERE id = @Id;";
                        var paramenters = new
                        {
                            Clave = tipoEstadistica.Clave.Value,
                            Nombre = tipoEstadistica.Nombre.Value,
                            Id = tipoEstadistica.Id
                        };
                        connection.Execute(query, paramenters, transaction);

                        //Delete List of Instrumento of TipoEstadistica
                        string queryDeleteInstrumentos =
                            "DELETE FROM estadistica_instrumento WHERE idEstadistica = @IdEstadistica;";
                        var parametersDeleteInstrumentos = new { IdEstadistica = tipoEstadistica.Id };
                        connection.Execute(queryDeleteInstrumentos, parametersDeleteInstrumentos, transaction);
                    
                        //Insert list of Instrumento
                        string queryInsertInstrumentos =
                            "INSERT INTO estadistica_instrumento(idEstadistica, idInstrumento) " +
                            "VALUES (@IdTipoEstadistica, @IdInstrumento)";
                        var parametersInsertInstrumento = tipoEstadistica.Instrumentos.Select(
                            row => new { IdTipoEstadistica = tipoEstadistica.Id, IdInstrumento = row.Id });
                        connection.Execute(queryInsertInstrumentos, parametersInsertInstrumento, transaction);

                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string queryDeleteInstrumentos =
                            "DELETE FROM estadistica_instrumento WHERE idEstadistica = @IdTipoEstadistica;";
                        var parametersInstrumento = new { IdTipoEstadistica = id};
                        connection.Execute(queryDeleteInstrumentos, parametersInstrumento, transaction);
                    
                        string queryDeleteTipoEstadistica = "DELETE FROM tipoEstadistica WHERE id = @Id";
                        var parametersTipoEstadistica = new { Id = id };
                        connection.Execute(queryDeleteTipoEstadistica, parametersTipoEstadistica, transaction);
                        
                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
        }
    }
}