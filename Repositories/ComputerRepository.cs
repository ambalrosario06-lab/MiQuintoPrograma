using MiQuintoPrograma.Data;
using MiQuintoPrograma.Models;

namespace MiQuintoPrograma.Repositories;

public class ComputerRepository(Database database)
{
    private readonly Database _database = database;

    public List<Computer> FindAll()
    {
        using var connection = _database.GetConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"
            SELECT uuid, name, brand, model, price
            FROM computer
            ORDER BY name DESC;
        ";

        List<Computer> computers = [];

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            computers.Add(
                new Computer()
                {
                    Uuid = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Brand = reader.GetString(2),
                    Model = reader.GetString(3),
                    Price = reader.GetInt32(4)
                }
            );
        }

        return computers;
    }

    public int Delete(Guid uuid)
    {
        using var connection = _database.GetConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"
            DELETE FROM computer
            WHERE uuid = @uuid;
        ";

        command.Parameters.AddWithValue("@uuid", uuid.ToString());

        int rowsDeleted = command.ExecuteNonQuery();
        return rowsDeleted;
    }

    public int Create(Guid uuid, string name, string brand, string model, int price)
    {
        using var connection = _database.GetConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"
            INSERT INTO
                computer(uuid, name, brand, model, price)
            VALUES
                (@uuid, @name, @brand, @model, @price);
        ";

        command.Parameters.AddWithValue("@uuid", uuid.ToString());
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@brand", brand);
        command.Parameters.AddWithValue("@model", model);
        command.Parameters.AddWithValue("@price", price);

        int rowsCreated = command.ExecuteNonQuery();
        return rowsCreated;
    }

    public int Update(Guid uuid, string name, string brand, string model, int price)
    {
        using var connection = _database.GetConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"
            UPDATE computer
            SET
                name = @name,
                brand = @brand,
                model = @model,
                price = @price
            WHERE uuid = @uuid;
        ";

        command.Parameters.AddWithValue("@uuid", uuid.ToString());
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@brand", brand);
        command.Parameters.AddWithValue("@model", model);
        command.Parameters.AddWithValue("@price", price);

        int rowsUpdated = command.ExecuteNonQuery();
        return rowsUpdated;
    }
}
