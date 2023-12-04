using System;
using System.Data;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=./sample.db;Version=3;";

        // Создание базы данных и таблицы (если они не существуют)
        CreateDatabase(connectionString);

        // Вставка данных в таблицу
        InsertData(connectionString, "John Doe", 25);

        // Чтение данных из таблицы
        ReadData(connectionString);

        Console.ReadLine();
    }

    static void CreateDatabase(string connectionString)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER);";

            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    static void InsertData(string connectionString, string name, int age)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string insertDataQuery = $"INSERT INTO Users (Name, Age) VALUES ('{name}', {age});";

            using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    static void ReadData(string connectionString)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string readDataQuery = "SELECT * FROM Users;";

            using (SQLiteCommand command = new SQLiteCommand(readDataQuery, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string name = reader["Name"].ToString();
                        int age = Convert.ToInt32(reader["Age"]);

                        Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
                    }
                }
            }
        }
    }
}
