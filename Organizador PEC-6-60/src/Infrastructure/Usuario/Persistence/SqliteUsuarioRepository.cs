using System;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Organizador_PEC_6_60.Domain.Usuario.Repository;
using Organizador_PEC_6_60.Domain.Usuario.ValueObjects;
using Organizador_PEC_6_60.Infrastructure.Share.Connections;

namespace Organizador_PEC_6_60.Infrastructure.Usuario.Persistence;

public class SqliteUsuarioRepository : UsuarioRepository
{
    private static SqliteUsuarioRepository _instance;

    private static readonly object _lock = new();

    private SqliteUsuarioRepository()
    {
    }

    public static SqliteUsuarioRepository Instance
    {
        get
        {
            if (_instance == null)
                lock (_lock)
                {
                    if (_instance == null) _instance = new SqliteUsuarioRepository();
                }

            return _instance;
        }
    }

    public Domain.Usuario.Model.Usuario FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Domain.Usuario.Model.Usuario LogIn(string username, string password)
    {
        using (IDbConnection connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var quuery = "SELECT * FROM usuario WHERE username = @Username AND password = @Password;";
            var parameters = new
            {
                Username = username,
                Password = password
            };
            var result = connection.QuerySingle(quuery, parameters);
            connection.Close();

            var usuario = new Domain.Usuario.Model.Usuario(
                new UsuarioUsername((string)result.username),
                new UsuarioPassword((string)result.password),
                new UsuarioNombre((string)result.nombre),
                new UsuarioApellidos((string)result.apellidos),
                (int)result.id
            );

            return usuario;
        }
    }

    public void Insert(Domain.Usuario.Model.Usuario newUsuario)
    {
        using (IDbConnection connection = DbConnection.GetSQLiteConnection())
        {
            if (connection == null)
                throw new SQLiteException("Base de datos no disponible.");

            var query = "INSERT INTO usuario (username, password, nombre, apellidos) " +
                        "VALUES (@Username, @Password, @Nombre, @Apellidos);";
            var parameters = new
            {
                Username = newUsuario.Username.Value,
                Password = newUsuario.Password.Value,
                Nombre = newUsuario.Nombre.Value,
                Apellidos = newUsuario.Apellidos.Value
            };
            connection.Execute(query, parameters);
            connection.Close();
        }
    }

    public void Update(Domain.Usuario.Model.Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}