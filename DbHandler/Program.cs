
using MySql.Data.MySqlClient;
using Service;

namespace DatabaseHandler;
public static class Program{
  public static void Main(string[] args){

    ToolBox.RunTime.ServiceClientName = "DatabaseService";
    ToolBox.AddService(new AccountDbManager());
    ToolBox.AddService(new PostDbManager());
    ToolBox.RunTime.Start("message_server", 20200);



      string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
      string? messageServerConnectionInformation = Environment.GetEnvironmentVariable("MsgConnection");
      string? activationName = Environment.GetEnvironmentVariable("ActivationName");

    if(connectionString == null){
      System.Console.WriteLine("Error getting connectionstring Enviroment variable!");
      return;
    }

    if(messageServerConnectionInformation == null){
      System.Console.WriteLine("Error getting Message Server connection Enviroment variable!");
      return;
    }

    if(activationName == null){
      System.Console.WriteLine("Error getting Activation Enviroment variable!");
      return;
    }

    System.Console.WriteLine("Checking connection to Database!");
    //- Open the connection.

    System.Console.WriteLine("Connecting to Message server");
    //- Open Connection to the Message server
  }
}

public class AccountDbManager : ServiceFunction
{
  readonly string connectionString = "Server=localhost;Database=your_database;User ID=your_user;Password=your_password;";

  public override void OnInit(FunctionConfig config)
  {
      config.FunctionName = "CreateAccount";
  }

  public override void OnRequest()
  {
    Console.WriteLine("Creating Account...");

    string username = "user3";
    string password = "password3";//should be hash

    // Attempt to save the account to the database
    bool accountCreated = SaveAccountToDatabase(username, password);

    if (accountCreated)
    {
        ToolBox.Request(RequestState.Finish);
    }
    else
    {
        ToolBox.Request(RequestState.Error);
    }

    ToolBox.Request("databaseService" ,"CreateAccount", [0xff]);


  }


    private bool SaveAccountToDatabase(string username, string password)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }
        catch (Exception)
        {
            ToolBox.Request(RequestState.Error);
            return false;
        }
    }
}
public class PostDbManager : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "PostDbManager";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Post Db Manager");
    }
}