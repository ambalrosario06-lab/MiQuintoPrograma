using Microsoft.Data.Sqlite;

namespace MiQuintoPrograma.Data;

public class Database
{
    private readonly string _connectionString;

    public Database(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";

        Initialize();
        Seed();
    }

    /// <summary>
    /// Se encarga de conectarse a Sqlite3.
    /// </summary>
    /// <returns>Una conexión de Sqlite.</returns>
    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    /// <summary>
    /// Inicializa las tablas necesarias para el funcionamiento de la aplicación.
    /// </summary>
    public void Initialize()
    {
        using var connection = GetConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"
            PRAGMA foreign_keys = ON;

            CREATE TABLE IF NOT EXISTS computer(
                id INTEGER PRIMARY KEY,
                uuid TEXT NOT NULL UNIQUE,
                name TEXT NOT NULL,
                brand TEXT NOT NULL,
                model TEXT NOT NULL,
                price REAL NOT NULL
            );
        ";

        command.ExecuteNonQuery();
    }

    public void Seed()
    {
        using var connection = GetConnection();
        using var existsCommand = connection.CreateCommand();

        existsCommand.CommandText = @"
            SELECT EXISTS(
                SELECT 1 FROM computer
            );
        ";

        bool exist = Convert.ToBoolean(existsCommand.ExecuteScalar());

        if (!exist)
        {
            using var seedCommand = connection.CreateCommand();

            seedCommand.CommandText = @"
                INSERT INTO
                    computer(uuid, name, brand, model, price)
                VALUES
                    ('bbf988f3-2109-4b49-8636-5547072a142f', 'Inspiron 15', 'Dell', '3520', 650),
                    ('c1f988f3-2109-4b49-8636-5547072a142f', 'MacBook Air', 'Apple', 'M2', 999),
                    ('d2f988f3-2109-4b49-8636-5547072a142f', 'Pavilion 15', 'HP', '15-eg', 720),
                    ('e3f988f3-2109-4b49-8636-5547072a142f', 'ThinkPad E14', 'Lenovo', 'Gen 5', 850),
                    ('f4f988f3-2109-4b49-8636-5547072a142f', 'Aspire 5', 'Acer', 'A515', 600);
            ";

            seedCommand.ExecuteNonQuery();
        }
    }
}