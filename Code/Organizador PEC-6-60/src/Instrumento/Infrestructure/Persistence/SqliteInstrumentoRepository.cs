using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Instrumento.Domain.Repository;
using Organizador_PEC_6_60.Instrumento.Domain.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Instrumento.Infrestructure.Persistence
{
    public class SqliteInstrumentoRepository : InstrumentoRepository
    {
        public IEnumerable<Domain.Model.Instrumento> SearchAll()
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM instrumento ORDER BY nombre;";
                var result = connection.Query(query).Select(
                    row => new Domain.Model.Instrumento(
                        new InstrumentoNombre((string)row.nombre),
                        (int)row.id
                    )
                );
                connection.Close();
                return result;
            }
        }

        public Domain.Model.Instrumento SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM instrumento WHERE id = @Id;";
                var parameters = new { Id = id };

                var result = connection.QuerySingle(query, parameters);
                connection.Close();
                Domain.Model.Instrumento instrumento =
                    new Domain.Model.Instrumento(new InstrumentoNombre((string)result.nombre), (int)result.id);
                return instrumento;
            }
        }

        public void Insert(Domain.Model.Instrumento newInstrumento)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "INSERT INTO instrumento (nombre) VALUES (@Nombre);";
                var parameters = new { Nombre = newInstrumento.Nombre.Value };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);
                    connection.Close();

                    if (affectedRows == 0)
                        throw new SQLiteException();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar el Instrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un instrumento registrado con ese nombre.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Update(Domain.Model.Instrumento instrumento)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "UPDATE instrumento SET nombre = @Nombre WHERE id = @Id;";
                var parameters = new { Nombre = instrumento.Nombre.Value, Id = instrumento.Id };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);
                    connection.Close();

                    if (affectedRows == 0)
                        throw new SQLiteException();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar el Instrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un instrumento registrada con ese nombre.";

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
                var parameters = new {  Id = id };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);
                    connection.Close();

                    if (affectedRows == 0)
                        throw new SQLiteException();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar el Instrumento, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un instrumento registrada con ese nombre.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }
    }
}