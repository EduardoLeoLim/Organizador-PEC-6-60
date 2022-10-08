using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Share.Infrastructure.Connections;

namespace Organizador_PEC_6_60.EntidadFederativa.Infrastructure.Persistence;

public class SqliteEntidadFederativaRepository : EntidadFederativaRepository
{
    private static SqliteEntidadFederativaRepository _instance;

    private static readonly object _lock = new();

    private SqliteEntidadFederativaRepository()
    {
    }

    public static SqliteEntidadFederativaRepository Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance == null)
                        _instance = new SqliteEntidadFederativaRepository();

            return _instance;
        }
    }

    public IEnumerable<Domain.Model.EntidadFederativa> SearchAll()
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "SELECT * FROM entidadFederativa ORDER BY folio;";
            var result = connection.Query(query).Select(
                item => new Domain.Model.EntidadFederativa(
                    new EntidadFederativaClave((int)item.folio),
                    new EntidadFederativaNombre((string)item.nombre),
                    (int)item.id
                )
            );
            connection.Close();

            return result;
        }
    }

    public Domain.Model.EntidadFederativa SeacrhById(int id)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "SELECT * FROM entidadFederativa WHERE id = @Id";
            var parameters = new { Id = id };
            var result = connection.QuerySingle(query, parameters);
            connection.Close();

            var entidadFederativa = new Domain.Model.EntidadFederativa(
                new EntidadFederativaClave((int)result.folio),
                new EntidadFederativaNombre((string)result.nombre),
                (int)result.id
            );

            return entidadFederativa;
        }
    }

    public void Insert(Domain.Model.EntidadFederativa newEntidadFederativa)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "INSERT INTO entidadFederativa (folio, nombre) VALUES (@Folio, @Nombre);";
            var parameters = new
            {
                Folio = newEntidadFederativa.Clave.Value,
                Nombre = newEntidadFederativa.Nombre.Value
            };

            try
            {
                var affectedRows = connection.Execute(query, parameters);
                connection.Close();

                if (affectedRows == 0)
                    throw new SQLiteException();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible registrar la Entidad Federativa, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

                connection.Close();

                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    public void Update(Domain.Model.EntidadFederativa entidadFederativa)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "UPDATE entidadFederativa SET folio = @Folio, nombre = @Nombre WHERE id = @Id;";
            var parameters = new
            {
                Folio = entidadFederativa.Clave.Value,
                Nombre = entidadFederativa.Nombre.Value,
                entidadFederativa.Id
            };

            try
            {
                var affectedRows = connection.Execute(query, parameters);
                connection.Close();

                if (affectedRows == 0)
                    throw new SQLiteException();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible actualizar la Entidad Federativa, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

                connection.Close();

                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    public void Delete(int id)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "DELETE FROM entidadFederativa WHERE id = @Id";
            var parameters = new { Id = id };
            connection.Execute(query, parameters);
            connection.Close();
        }
    }
}