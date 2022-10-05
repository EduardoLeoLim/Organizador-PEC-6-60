using System;
using System.Configuration;
using System.Data.SQLite;

namespace Organizador_PEC_6_60.Infrastructure.Share.Connections;

public class DbConnection
{
    private static readonly string ID_CONNECTION_SQLITE = "SQLite_Default";

    private static string GetConnectionString(string id)
    {
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }

    public static SQLiteConnection GetSQLiteConnection()
    {
        SQLiteConnection connection = null;
        try
        {
            var connectionString = GetConnectionString(ID_CONNECTION_SQLITE);
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON", connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            connection?.Close();
            connection = null;
        }

        return connection;
    }
}