using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Share.Infrastructure.Connections;
using Organizador_PEC_6_60.TipoEstadistica.Domain.Repository;
using Organizador_PEC_6_60.TipoEstadistica.Domain.ValueObjects;
using Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoEstadistica.Infrastructure.Persistence;

public class SqliteTipoEstadisticaRepository : TipoEstadisticaRepository
{
    private static SqliteTipoEstadisticaRepository _instance;

    private static readonly object _lock = new();

    private SqliteTipoEstadisticaRepository()
    {
    }

    public static SqliteTipoEstadisticaRepository Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance == null)
                        _instance = new SqliteTipoEstadisticaRepository();

            return _instance;
        }
    }

    public IEnumerable<Domain.Model.TipoEstadistica> SearchAll()
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var collection =
                new List<Domain.Model.TipoEstadistica>();

            var queryTipoEstadistica = "SELECT * FROM tipoEstadistica ORDER BY folio;";
            var queryInstrumentos =
                "SELECT i.id, i.nombre FROM instrumento AS i INNER JOIN estadistica_instrumento AS ei " +
                "ON (i.id = ei.idInstrumento) " +
                "WHERE ei.idEstadistica = @IdTipoEstadistica;";

            var resultTipoEstadistica = connection.Query(queryTipoEstadistica);
            foreach (var item in resultTipoEstadistica)
            {
                var parameters = new { IdTipoEstadistica = (int)item.id };
                var listInstrumentos = connection
                    .Query(queryInstrumentos, parameters)
                    .Select(row => new TipoInstrumento.Domain.Model.TipoInstrumento(
                        new TipoInstrumentoNombre((string)row.nombre),
                        (int)row.id)
                    ).ToList();

                var tipoEstadistica = new Domain.Model.TipoEstadistica(
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
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var queryTipoEstadistica = "SELECT * FROM tipoEstadistica WHERE id = @Id;";
            var paramentersTipoEstadistica = new { Id = idTipoEstadistica };
            var resultTipoEstadistica = connection.QuerySingle(queryTipoEstadistica, paramentersTipoEstadistica);

            var queryInstrumentos =
                "SELECT i.id, i.nombre FROM instrumento AS i INNER JOIN estadistica_instrumento AS ei " +
                "ON (i.id = ei.idInstrumento) " +
                "WHERE ei.idEstadistica = @IdTipoEstadistica;";
            var paramentersInstrumentos = new { IdTipoEstadistica = (int)resultTipoEstadistica.id };

            var instrumentos = connection.Query(queryInstrumentos, paramentersInstrumentos)
                .Select(row =>
                    new TipoInstrumento.Domain.Model.TipoInstrumento(
                        new TipoInstrumentoNombre((string)row.nombre),
                        (int)row.id
                    )
                ).ToList();
            connection.Close();

            var tipoEstadistica = new Domain.Model.TipoEstadistica(
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
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    //Insert TipoEstadistica
                    var queryTipoEstadistica =
                        "INSERT INTO tipoEstadistica (folio, nombre) VALUES (@Clave, @Nombre) RETURNING id;";
                    var parametersTipoEstadistica = new
                    {
                        Clave = newTipoEstadistica.Clave.Value,
                        Nombre = newTipoEstadistica.Nombre.Value
                    };

                    var resultTipoEstadistica = connection.QuerySingle(queryTipoEstadistica,
                        parametersTipoEstadistica,
                        transaction);

                    //Insert list of TipoInstrumento
                    var queryInsertInstrumentos =
                        "INSERT INTO estadistica_instrumento(idEstadistica, idInstrumento) " +
                        "VALUES (@IdTipoEstadistica, @IdInstrumento)";
                    var parametersInsertInstrumento = newTipoEstadistica.Instrumentos.Select(
                        row => new
                        {
                            IdTipoEstadistica = (int)resultTipoEstadistica.id,
                            IdInstrumento = row.Id
                        });

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
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    //Update TipoEstadistica
                    var query = "UPDATE tipoEstadistica SET folio = @Clave, nombre = @Nombre WHERE id = @Id;";
                    var paramenters = new
                    {
                        Clave = tipoEstadistica.Clave.Value,
                        Nombre = tipoEstadistica.Nombre.Value,
                        tipoEstadistica.Id
                    };
                    connection.Execute(query, paramenters, transaction);

                    //Delete List of TipoInstrumento of TipoEstadistica
                    var queryDeleteInstrumentos =
                        "DELETE FROM estadistica_instrumento WHERE idEstadistica = @IdEstadistica;";
                    var parametersDeleteInstrumentos = new { IdEstadistica = tipoEstadistica.Id };
                    connection.Execute(queryDeleteInstrumentos, parametersDeleteInstrumentos, transaction);

                    //Insert list of TipoInstrumento
                    var queryInsertInstrumentos =
                        "INSERT INTO estadistica_instrumento(idEstadistica, idInstrumento) " +
                        "VALUES (@IdTipoEstadistica, @IdInstrumento)";
                    var parametersInsertInstrumento = tipoEstadistica.Instrumentos.Select(
                        row => new
                        {
                            IdTipoEstadistica = tipoEstadistica.Id,
                            IdInstrumento = row.Id
                        });

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
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var queryDeleteInstrumentos =
                        "DELETE FROM estadistica_instrumento WHERE idEstadistica = @IdTipoEstadistica;";
                    var parametersInstrumento = new { IdTipoEstadistica = id };
                    connection.Execute(queryDeleteInstrumentos, parametersInstrumento, transaction);

                    var queryDeleteTipoEstadistica = "DELETE FROM tipoEstadistica WHERE id = @Id";
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