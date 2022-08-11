using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Organizador_PEC_6_60.EntidadFederativa.Domain.ValueObjects;
using Organizador_PEC_6_60.Municipio.Domain.Repository;
using Organizador_PEC_6_60.Municipio.Domain.ValueObjects;
using Organizador_PEC_6_60.Resources.Database;

namespace Organizador_PEC_6_60.Municipio.Infrestructure.Persistence
{
    public class SqliteMunicipioRepository : MunicipioRepository
    {
        public IEnumerable<Domain.Model.Municipio> SearchAll(int idEntidadFederativa)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT m.id as IdMunicipio, m.folio as ClaveMunicipio, m.nombre AS NombreMunicipio, " +
                               "eF.id AS IdEntidad, eF.folio AS ClaveEntidad, ef.nombre AS NombreEntidad " +
                               "FROM municipio AS m INNER JOIN entidadFederativa eF on eF.id = m.idEntidadFederativa " +
                               "WHERE eF.id = @IdEntidadFederativa ORDER BY ClaveEntidad;";
                var paramenters = new
                {
                    IdEntidadFederativa = idEntidadFederativa
                };

                var result = connection.Query(query, paramenters).Select(
                    row => new Domain.Model.Municipio(
                        new MunicipioClave((int)row.ClaveMunicipio),
                        new MunicipioNombre((string)row.NombreMunicipio),
                        new EntidadFederativa.Domain.Model.EntidadFederativa(
                            new EntidadFederativaClave((int)row.ClaveEntidad),
                            new EntidadFederativaNombre((string)row.NombreEntidad),
                            (int)row.IdEntidad),
                        (int)row.IdMunicipio
                    )
                );

                connection.Close();

                return result;
            }
        }

        public Domain.Model.Municipio SearchById(int id)
        {
            using (SQLiteConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "SELECT m.id as IdMunicipio, m.folio as ClaveMunicipio, m.nombre AS NombreMunicipio, " +
                               "eF.id AS IdEntidad, eF.folio AS ClaveEntidad, ef.nombre AS NombreEntidad " +
                               "FROM municipio AS m INNER JOIN entidadFederativa eF on eF.id = m.idEntidadFederativa " +
                               "WHERE m.id = @IdMunicipio;";
                var parameters = new
                {
                    IdMunicipio = id
                };

                var result = connection.QuerySingle(query, parameters);
                connection.Close();

                EntidadFederativa.Domain.Model.EntidadFederativa entidadFederativa =
                    new EntidadFederativa.Domain.Model.EntidadFederativa(
                        new EntidadFederativaClave((int)result.ClaveEntidad),
                        new EntidadFederativaNombre((string)result.NombreEntidad),
                        (int)result.IdEntidad);
                Domain.Model.Municipio municipio =
                    new Domain.Model.Municipio(new MunicipioClave((int)result.ClaveMunicipio),
                        new MunicipioNombre((string)result.NombreMunicipio), entidadFederativa,
                        (int)result.IdMunicipio);

                return municipio;
            }
        }

        public void Insert(Domain.Model.Municipio newMunicipio)
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
                    IdEntidadFederativa = newMunicipio.EntidadFederativa.Id
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

        public void Update(Domain.Model.Municipio municipio)
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
                    IdEntidadFederativa = municipio.EntidadFederativa.Id
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