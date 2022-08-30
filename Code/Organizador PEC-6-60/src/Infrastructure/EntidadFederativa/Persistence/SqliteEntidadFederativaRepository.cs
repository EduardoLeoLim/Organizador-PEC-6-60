using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Domain.EntidadFederativa.Repository;
using Organizador_PEC_6_60.Domain.EntidadFederativa.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Infrastructure.EntidadFederativa.Persistence
{
    public class SqliteEntidadFederativaRepository : EntidadFederativaRepository
    {
        public IEnumerable<Domain.EntidadFederativa.Model.EntidadFederativa> SearchAll()
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM entidadFederativa ORDER BY folio;";
                var result = connection.Query(query).Select(
                    row => new Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa(
                        new EntidadFederativaClave((int)row.folio),
                        new EntidadFederativaNombre((string)row.nombre),
                        (int)row.id
                    )
                );
                connection.Close();
                return result;
            }
        }

        public Domain.EntidadFederativa.Model.EntidadFederativa SeacrhById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM entidadFederativa WHERE id = @Id";
                var parameters = new { Id = id };
                var result = connection.QuerySingle(query, parameters);
                connection.Close();
                Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa = new Organizador_PEC_6_60.Domain.EntidadFederativa.Model.EntidadFederativa(
                    new EntidadFederativaClave((int)result.folio),
                    new EntidadFederativaNombre((string)result.nombre), (int)result.id);
                return entidadFederativa;
            }
        }

        public void Insert(Domain.EntidadFederativa.Model.EntidadFederativa newEntidadFederativa)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "INSERT INTO entidadFederativa (folio, nombre) VALUES (@Folio, @Nombre);";
                var parameters = new
                    { Folio = newEntidadFederativa.Clave.Value, Nombre = newEntidadFederativa.Nombre.Value };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);
                    connection.Close();
                    
                    if (affectedRows == 0)
                        throw new SQLiteException();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar la Entidad Federativa, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Update(Domain.EntidadFederativa.Model.EntidadFederativa entidadFederativa)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "UPDATE entidadFederativa SET folio = @Folio, nombre = @Nombre WHERE id = @Id;";
                var parameters = new
                {
                    Folio = entidadFederativa.Clave.Value, Nombre = entidadFederativa.Nombre.Value,
                    Id = entidadFederativa.Id
                };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);
                    connection.Close();
                    
                    if (affectedRows == 0)
                        throw new SQLiteException();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar la Entidad Federativa, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

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

                string query = "DELETE FROM entidadFederativa WHERE id = @Id";
                var parameters = new { Id = id };
                connection.Execute(query, parameters);
                connection.Close();
            }
        }
    }
}