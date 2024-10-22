using Service;

ToolBox.RunTime.ServiceClientName = "Database";
ToolBox.RunTime.Start("127.0.0.1", 20200);

class CreateUser : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "CreateUser";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Creating New user!");
    }
}
