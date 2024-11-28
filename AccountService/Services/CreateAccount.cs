using RabbitMqDefault.interfaces;

namespace AccountService;
public class CreateAccount: AService{
  public CreateAccount(){
    // database = new MySqlDatabase("accountuser", "accountpassword","api_network", "AccountDb");
  }

  public override bool OnInit()
  {
    //- Could be used to create instance of different elements
    return true;
  }

  public override ServiceState OnInvoke(byte[] stream)
  {
    //- This is called when the message broker has been called!
    //- stream is the message from the broker -> caller.
    System.Console.WriteLine("Creating Account Invoked!");

    string username = "DemoAccount";
    string password = "DemoPassword";

    Dictionary<string, object> columns = new Dictionary<string, object>
    {
        { "Username", username },
        { "Password", password },
        { "Activity", 0 }
    };

    string insertQuery = database.CreateInsertQuery("Accounts", columns);

    int rowsAffected = database.ExecuteNonQuery(insertQuery);

    if (rowsAffected > 0)
    {
        return ServiceState.Ok;
    }
    else
    {
        return ServiceState.Error;
    }
  }

  public override void onFallback()
  {
    //- Could be used for some cleaning. 
  }

  public MySqlDatabase database { get; set; }



}