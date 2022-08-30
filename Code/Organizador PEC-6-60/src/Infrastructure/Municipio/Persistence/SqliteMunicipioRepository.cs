using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Infrastructure.Municipio.Persistence
{
    public class SqliteMunicipioRepository : MunicipioRepository
    {
        public IEnumerable<Domain.Municipio.Model.Municipio> SearchAll(int idEntidadFederativa)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query =
                    "SELECT m.id as Id, m.folio as Clave, m.nombre AS Nombre, m.idEntidadFederativa AS IdEntidadFederativa " +
                    "FROM municipio AS m WHERE m.idEntidadFederativa = @IdEntidadFederativa;";
                var paramenters = new
                {
                    IdEntidadFederativa = idEntidadFederativa
                };

                var resut = connection.Query(query, paramenters);
                var municipios = resut.Select(row => new Organizador_PEC_6_60.Domain.Municipio.Model.Municipio(
                        new MunicipioClave((int)row.Clave),
                        new MunicipioNombre((string)row.Nombre),
                        (int)row.IdEntidadFederativa,
                        id: (int)row.Id
                    )
                );

                connection.Close();

                return municipios;
            }
        }

        public Domain.Municipio.Model.Municipio SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query =
                    "SELECT m.id as Id, m.folio as Clave, m.nombre AS Nombre, m.idEntidadFederativa AS IdEntidadFederativa " +
                    "FROM municipio AS m WHERE m.id = @IdMunicipio;";
                var parameters = new
                {
                    IdMunicipio = id
                };

                var result = connection.QuerySingle(query, parameters);
                connection.Close();

                Organizador_PEC_6_60.Domain.Municipio.Model.Municipio municipio = new Organizador_PEC_6_60.Domain.Municipio.Model.Municipio(
                    new MunicipioClave((int)result.Clave), new MunicipioNombre((string)result.Nombre),
                    (int)result.IdEntidadFederativa, id: (int)result.Id);

                return municipio;
            }
        }

        public void Insert(Domain.Municipio.Model.Municipio newMunicipio)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query =
                    "INSERT INTO municipio (folio, nombre, identidadfederativa) VALUES (@Clave, @Nombre, @IdEntidadFederativa);";
                var paramenters = new
                {
                    Clave = newMunicipio.Clave.Value,
                    Nombre = newMunicipio.Nombre.Value,
                    IdEntidadFederativa = newMunicipio.IdEntidadFederativa
                };

                try
                {
                    int affectedRows = connection.Execute(query, paramenters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible registrar el Municipio, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay una municipio registrado con esa clave.";

                    connection.Close();

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        public void Update(Domain.Municipio.Model.Municipio municipio)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "UPDATE municipio SET folio = @Clave, nombre = @Nombre, " +
                               "idEntidadFederativa = @IdEntidadFederativa WHERE id = @IdMunicipio;";
                var parameters = new
                {
                    IdMunicipio = municipio.Id,
                    Clave = municipio.Clave.Value,
                    Nombre = municipio.Nombre.Value,
                    IdEntidadFederativa = municipio.IdEntidadFederativa
                };

                try
                {
                    int affectedRows = connection.Execute(query, parameters);

                    if (affectedRows == 0)
                        throw new SQLiteException();

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    string errorMessage = "No fue posible actualizar el municipio, Intentalo más tarde.";
                    if (ex.ErrorCode == 19)
                        errorMessage = "Ya hay un municipio registrado con esa clave.";

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

                string query = "DELETE FROM municipio WHERE id = @Id";
                var parameters = new { Id = id };
                connection.Execute(query, parameters);
                connection.Close();
            }
        }
    }
}