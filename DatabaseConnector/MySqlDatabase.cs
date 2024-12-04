using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDatabase
{
    private string _connectionString;
    private MySqlConnection _connection;

    public MySqlDatabase(string _connectionString)
    {
      this._connectionString = _connectionString;
      OpenConnection();

    }

     public bool IsConnected => _connection != null && _connection.State == ConnectionState.Open;

    public void OpenConnection()
    {
      System.Console.WriteLine("Trying To connect");
      for (int i = 0; i < 10; i++)
      {
        try
        {
          _connection = new MySqlConnection(_connectionString);
          _connection.Open();
          System.Console.WriteLine("Connected!");
          break;
        }
        catch (Exception ex)
        {
          System.Console.WriteLine(ex.Message);
          Console.Write(".");
        }
      }
    }

    public DataTable ExecuteQuery(string query)
    {
        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, _connection))
        {
            try
            {
                DataTable result = new DataTable();
                dataAdapter.Fill(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
                return null;
            }
        }
    }

    public int ExecuteNonQuery(string query)
    {
        using (MySqlCommand command = new MySqlCommand(query, _connection))
        {
            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
                return 0;
            }
        }
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
