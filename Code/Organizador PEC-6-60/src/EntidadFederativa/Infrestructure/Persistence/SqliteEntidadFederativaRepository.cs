using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Organizador_PEC_6_60.EntidadFederativa.Domain.Repository;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.EntidadFederativa.Infrestructure.Persistence
{
    public class SqliteEntidadFederativaRepository : EntidadFederativaRepository
    {
        public IEnumerable<Domain.Model.EntidadFederativa> SearchAll()
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM entidadFederativa ORDER BY folio;";
                var result = connection.Query(query).Select(
                    row => new Domain.Model.EntidadFederativa(
                        new EntidadFederativaClave((int)row.folio),
                        new EntidadFederativaNombre((string)row.nombre),
                        (int)row.id
                    )
                );
                connection.Close();
                return result;
            }
        }

        public Domain.Model.EntidadFederativa SeacrhById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT * FROM entidadFederativa WHERE id = @Id";
                var parameters = new { Id = id };
                var result = connection.QuerySingle(query, parameters);
                connection.Close();
                Domain.Model.EntidadFederativa entidadFederativa = new Domain.Model.EntidadFederativa(
                    new EntidadFederativaClave((int)result.folio),
                    new EntidadFederativaNombre((string)result.nombre), (int)result.id);
                return entidadFederativa;
            }
        }

        public void Insert(Domain.Model.EntidadFederativa newEntidadFederativa)
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
                    connection.Execute(query, parameters);
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar la Entidad Federativa, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }

                connection.Close();
            }
        }

        public void Update(Domain.Model.EntidadFederativa entidadFederativa)
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
                    connection.Execute(query, parameters);
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar la Entidad Federativa, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una entidad federativa registrada con esa clave.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }

                connection.Close();
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
            }
        }
    }
}