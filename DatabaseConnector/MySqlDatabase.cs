using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDatabase
{
    private string _connectionString;

    public MySqlDatabase(string username, string password, string host = "localhost", string database = "")
    {
        _connectionString = $"Server={host};Database={database};User ID={username};Password={password};";
    }
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
    public string CreateInsertQuery(string tableName, Dictionary<string, object> columns)
    {
        string columnNames = string.Join(", ", columns.Keys);

        // Format values to handle different types (e.g., strings, numbers)
        string values = string.Join(", ", columns.Values.Select(value =>
            value is string ? $"'{value}'" : value.ToString()));

        return $"INSERT INTO {tableName} ({columnNames}) VALUES ({values});";
    }
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
