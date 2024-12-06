using System.Text;
using RabbitMqDefault.interfaces;

namespace AccountService.Services;
public class CreateAccount: AService{
  public CreateAccount()
    {
      //- This should be removed to Environment variable
      string _connectionString = $"Server=account_db;Database=AccountDb;User ID=accountuser;Password=accountpassword;";
      database = new MySqlDatabase(_connectionString);
    }

  public override bool OnInit()
  {
    //- Could be used to create instance of different elements
    System.Console.WriteLine("Init invoked!");
    return true;
  }

  public override ServiceState OnInvoke(byte[] stream)
  {
    //- This is called when the message broker has been called!
    //- stream is the message from the broker -> caller.
    System.Console.WriteLine("Creating Account Invoked!");
    string[] strings = Encoding.UTF8.GetString(stream).Split(",");

    if(strings.Length < 2)
      return ServiceState.Error;

    Dictionary<string, object> columns = new Dictionary<string, object>
    {
        { "Username", strings[0] },
        { "Password", strings[1] },
        { "Activity", 0 }
    };

    string insertQuery = database.CreateInsertQuery("Accounts", columns);
    int rowsAffected = database.ExecuteNonQuery(insertQuery);

    if (rowsAffected > 0)
    {
      System.Console.WriteLine("Saved to database!");
      return ServiceState.Ok;
    }
    else
    {
      System.Console.WriteLine("Error saving to database");
      return ServiceState.Error;
    }
  }

  public override void onFallback()
  {
    //- Could be used for some cleaning. 
    System.Console.WriteLine("Fallback called!");
  }

  public MySqlDatabase database { get; set; }
}