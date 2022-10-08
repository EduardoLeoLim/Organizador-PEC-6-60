using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Share.Infrastructure.Connections;
using Organizador_PEC_6_60.TipoInstrumento.Domain.Repository;
using Organizador_PEC_6_60.TipoInstrumento.Domain.ValueObjects;

namespace Organizador_PEC_6_60.TipoInstrumento.Infrastructure.Persistence;

public class SqliteTipoInstrumentoRepository : TipoInstrumentoRepository
{
    private static SqliteTipoInstrumentoRepository _instance;

    private static readonly object _lock = new();

    private SqliteTipoInstrumentoRepository()
    {
    }

    public static SqliteTipoInstrumentoRepository Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance == null)
                        _instance = new SqliteTipoInstrumentoRepository();

            return _instance;
        }
    }

    public IEnumerable<Domain.Model.TipoInstrumento> SearchAll()
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "SELECT * FROM instrumento ORDER BY nombre;";
            var result = connection.Query(query).Select(
                row => new Domain.Model.TipoInstrumento(
                    new TipoInstrumentoNombre((string)row.nombre),
                    (int)row.id
                )
            );
            connection.Close();

            return result;
        }
    }

    public Domain.Model.TipoInstrumento SearchById(int id)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "SELECT * FROM instrumento WHERE id = @Id;";
            var parameters = new { Id = id };

            var result = connection.QuerySingle(query, parameters);
            connection.Close();
            var tipoInstrumento = new Domain.Model.TipoInstrumento(
                new TipoInstrumentoNombre((string)result.nombre),
                (int)result.id
            );

            return tipoInstrumento;
        }
    }

    public void Insert(Domain.Model.TipoInstrumento newTipoInstrumento)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "INSERT INTO instrumento (nombre) VALUES (@Nombre);";
            var parameters = new { Nombre = newTipoInstrumento.Nombre.Value };

            try
            {
                var affectedRows = connection.Execute(query, parameters);

                if (affectedRows == 0)
                    throw new SQLiteException();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible registrar el TipoInstrumento, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay un tipoInstrumento registrado con ese nombre.";

                connection.Close();

                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    public void Update(Domain.Model.TipoInstrumento tipoInstrumento)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "UPDATE instrumento SET nombre = @Nombre WHERE id = @Id;";
            var parameters = new { Nombre = tipoInstrumento.Nombre.Value, tipoInstrumento.Id };

            try
            {
                var affectedRows = connection.Execute(query, parameters);

                if (affectedRows == 0)
                    throw new SQLiteException();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible actualizar el TipoInstrumento, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay un tipoInstrumento registrada con ese nombre.";

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

            var query = "DELETE FROM instrumento WHERE id = @Id;";
            var parameters = new { Id = id };

            try
            {
                var affectedRows = connection.Execute(query, parameters);

                if (affectedRows == 0)
                    throw new SQLiteException();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible eliminar el TipoInstrumento, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage =
                        "No es posible eliminar el tipoInstrumento ya que esta asociado con un tipo de estadística o formato PEC 6-60";

                connection.Close();

                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}