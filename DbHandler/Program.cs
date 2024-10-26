
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
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "AccountDbManager";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Account Db Manager");
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