using Service;

ToolBox.RunTime.ServiceClientName = "AccountService";
//- Add the services
ToolBox.AddService(new CreateAccount());
ToolBox.AddService(new DeleteAccount());
ToolBox.AddService(new Login());
ToolBox.AddService(new Logout());
//- End Add Service
ToolBox.RunTime.Start("127.0.0.1", 20200);


public class CreateAccount : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "CreateAccount";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Creating Account");
    }
}

public class DeleteAccount : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "DeleteAccount";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Delete Account");
    }
}

public class Login : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "Login";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Login");
    }
}

public class Logout : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "Logout";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Logout");
    }
}