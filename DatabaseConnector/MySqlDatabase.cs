using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDatabase
{
    private string _connectionString;

    // Constructor: Initialize with the database username and password
    public MySqlDatabase(string username, string password, string host = "localhost", string database = "")
    {
        _connectionString = $"Server={host};Database={database};User ID={username};Password={password};";
    }

    // Function to execute a query (select, insert, update, delete, etc.)
    public DataTable ExecuteQuery(string query)
    {
        DataTable result = new DataTable();

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, connection);
                dataAdapter.Fill(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        return result;
    }

    // Function to execute a non-query (insert, update, delete)
    public int ExecuteNonQuery(string query)
    {
        int rowsAffected = 0;

        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        return rowsAffected;
    }

    // Function to easily create an SQL insert statement
    public string CreateInsertQuery(string tableName, Dictionary<string, object> columns)
    {
        string columnNames = string.Join(", ", columns.Keys);

        // Format values to handle different types (e.g., strings, numbers)
        string values = string.Join(", ", columns.Values.Select(value =>
            value is string ? $"'{value}'" : value.ToString()));

        return $"INSERT INTO {tableName} ({columnNames}) VALUES ({values});";
    }

    // Function to easily create an SQL select statement
    public string CreateSelectQuery(string tableName, List<string> columns = null, string whereClause = "")
    {
        string columnNames = columns != null && columns.Count > 0 ? string.Join(", ", columns) : "*";
        string query = $"SELECT {columnNames} FROM {tableName}";

        if (!string.IsNullOrEmpty(whereClause))
        {
            query += " WHERE " + whereClause;
        }

        return query;
    }
}
