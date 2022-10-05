using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Organizador_PEC_6_60.Domain.Municipio.Repository;
using Organizador_PEC_6_60.Domain.Municipio.ValueObjects;
using Organizador_PEC_6_60.Infrastructure.Share.Connections;

namespace Organizador_PEC_6_60.Infrastructure.Municipio.Persistence;

public class SqliteMunicipioRepository : MunicipioRepository
{
    private static SqliteMunicipioRepository _instance;

    private static readonly object _lock = new();

    private SqliteMunicipioRepository()
    {
    }

    public static SqliteMunicipioRepository Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                {
                    if (_instance == null) _instance = new SqliteMunicipioRepository();
                }

            return _instance;
        }
    }

    public IEnumerable<Domain.Municipio.Model.Municipio> SearchAll(int idEntidadFederativa)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query =
                "SELECT m.id as Id, m.folio as Clave, m.nombre AS Nombre, m.idEntidadFederativa AS IdEntidadFederativa " +
                "FROM municipio AS m WHERE m.idEntidadFederativa = @IdEntidadFederativa;";
            var paramenters = new
            {
                IdEntidadFederativa = idEntidadFederativa
            };

            var resut = connection.Query(query, paramenters);
            var municipios = resut.Select(
                item => new Domain.Municipio.Model.Municipio(
                    new MunicipioClave((int)item.Clave),
                    new MunicipioNombre((string)item.Nombre),
                    (int)item.IdEntidadFederativa,
                    (int)item.Id
                )
            );

            connection.Close();

            return municipios;
        }
    }

    public Domain.Municipio.Model.Municipio SearchById(int id)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query =
                "SELECT m.id as Id, m.folio as Clave, m.nombre AS Nombre, m.idEntidadFederativa AS IdEntidadFederativa " +
                "FROM municipio AS m WHERE m.id = @IdMunicipio;";
            var parameters = new
            {
                IdMunicipio = id
            };

            var result = connection.QuerySingle(query, parameters);
            connection.Close();

            var municipio =
                new Domain.Municipio.Model.Municipio(
                    new MunicipioClave((int)result.Clave),
                    new MunicipioNombre((string)result.Nombre),
                    (int)result.IdEntidadFederativa,
                    (int)result.Id
                );

            return municipio;
        }
    }

    public void Insert(Domain.Municipio.Model.Municipio newMunicipio)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query =
                "INSERT INTO municipio (folio, nombre, identidadfederativa) VALUES (@Clave, @Nombre, @IdEntidadFederativa);";
            var paramenters = new
            {
                Clave = newMunicipio.Clave.Value,
                Nombre = newMunicipio.Nombre.Value,
                newMunicipio.IdEntidadFederativa
            };

            try
            {
                var affectedRows = connection.Execute(query, paramenters);

                if (affectedRows == 0)
                    throw new SQLiteException();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible registrar el Municipio, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay una municipio registrado con esa clave.";

                connection.Close();

                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    public void Update(Domain.Municipio.Model.Municipio municipio)
    {
        using (var connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "UPDATE municipio SET folio = @Clave, nombre = @Nombre, " +
                        "idEntidadFederativa = @IdEntidadFederativa WHERE id = @IdMunicipio;";
            var parameters = new
            {
                IdMunicipio = municipio.Id,
                Clave = municipio.Clave.Value,
                Nombre = municipio.Nombre.Value,
                municipio.IdEntidadFederativa
            };

            try
            {
                var affectedRows = connection.Execute(query, parameters);

                if (affectedRows == 0)
                    throw new SQLiteException();

                connection.Close();
            }
            catch (SQLiteException ex)
            {
                var errorMessage = "No fue posible actualizar el municipio, Intentalo más tarde.";
                if (ex.ErrorCode == 19)
                    errorMessage = "Ya hay un municipio registrado con esa clave.";

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

            var query = "DELETE FROM municipio WHERE id = @Id";
            var parameters = new { Id = id };
            connection.Execute(query, parameters);
            connection.Close();
        }
    }
}