using System;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Organizador_PEC_6_60.Resources.Database;
using Organizador_PEC_6_60.Usuario.Domain.Repository;
using Organizador_PEC_6_60.Usuario.Domain.ValueObjects;

namespace Organizador_PEC_6_60.Usuario.Infrestructure.Persistence
{
    public class SqliteUsuarioRepository : UsuarioRepository
    {
        public Domain.Model.Usuario FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.Model.Usuario LogIn(string username, string password)
        {
            using (IDbConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string quuery = "SELECT * FROM usuario WHERE username = @Username AND password = @Password;";
                var parameters = new
                {
                    Username = username,
                    Password = password
                };
                dynamic result = connection.QuerySingle(quuery, parameters);
                Domain.Model.Usuario usuario = new Domain.Model.Usuario(
                    new UsuarioUsername((string)result.username),
                    new UsuarioPassword((string)result.password),
                    new UsuarioNombre((string)result.nombre),
                    new UsuarioApellidos((string)result.apellidos),
                    (int)result.id
                );
                return usuario;
            }
        }

        public void Insert(Domain.Model.Usuario newUsuario)
        {
            using (IDbConnection connection = DbConnection.GetSQLiteConnection())
            {
                if (connection == null)
                    throw new SQLiteException("Base de datos no disponible.");

                string query = "INSERT INTO usuario (username, password, nombre, apellidos) " +
                               "VALUES (@Username, @Password, @Nombre, @Apellidos);";
                var parameters = new
                {
                    newUsuario.Username.Username,
                    newUsuario.Password.Password,
                    newUsuario.Nombre.Nombre,
                    newUsuario.Apellidos.Apellidos
                };
                connection.Execute(query, parameters);
            }
        }

        public void Update(Domain.Model.Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Domain.Model.Usuario usuario)
        {
            throw new System.NotImplementedException();
        }
    }
}