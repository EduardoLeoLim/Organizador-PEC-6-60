using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Domain.TipoInstrumento.Repository;
using Organizador_PEC_6_60.Domain.TipoInstrumento.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Infrastructure.TipoInstrumento.Persistence
{
    public class SqliteTipoInstrumentoRepository : TipoInstrumentoRepository
    {
        private static SqliteTipoInstrumentoRepository _instance;

        private static readonly object _lock = new object();

        private SqliteTipoInstrumentoRepository()
        {
        }

        public static SqliteTipoInstrumentoRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqliteTipoInstrumentoRepository();
                        }
                    }
                }

                return _instance;
            }
        }

        public IEnumerable<Domain.TipoInstrumento.Model.TipoInstrumento> SearchAll()
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM instrumento ORDER BY nombre;";
                var result = connection.Query(query).Select(
                    row => new Domain.TipoInstrumento.Model.TipoInstrumento(
                        new TipoInstrumentoNombre((string)row.nombre),
                        (int)row.id
                    )
                );
                connection.Close();

                return result;
            }
        }

        public Domain.TipoInstrumento.Model.TipoInstrumento SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM instrumento WHERE id = @Id;";
                var parameters = new { Id = id };

                var result = connection.QuerySingle(query, parameters);
                connection.Close();
                Domain.TipoInstrumento.Model.TipoInstrumento tipoInstrumento =
                    new Domain.TipoInstrumento.Model.TipoInstrumento(
                        new TipoInstrumentoNombre((string)result.nombre),
                        (int)result.id
                    );

                return tipoInstrumento;
            }
        }

        public void Insert(Domain.TipoInstrumento.Model.TipoInstrumento newTipoInstrumento)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "INSERT INTO instrumento (nombre) VALUES (@Nombre);";
                var parameters = new { Nombre = newTipoInstrumento.Nombre.Value };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar el TipoInstrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un tipoInstrumento registrado con ese nombre.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Update(Domain.TipoInstrumento.Model.TipoInstrumento tipoInstrumento)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "UPDATE instrumento SET nombre = @Nombre WHERE id = @Id;";
                var parameters = new { Nombre = tipoInstrumento.Nombre.Value, Id = tipoInstrumento.Id };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar el TipoInstrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un tipoInstrumento registrada con ese nombre.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "DELETE FROM instrumento WHERE id = @Id;";
                var parameters = new { Id = id };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible eliminar el TipoInstrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage =
                            "No es posible eliminar el tipoInstrumento ya que esta asociado con un tipo de estadística o formato PEC 6-60";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }
    }
}