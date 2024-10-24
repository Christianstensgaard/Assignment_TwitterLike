using Service;

ToolBox.RunTime.ServiceClientName = "DB";
ToolBox.RunTime.AddService(new CreateUser());
ToolBox.RunTime.AddService(new UpdateUser());
ToolBox.RunTime.Start("127.0.0.1", 20200);

class CreateUser : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "CreateUser";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Creating User!");
    }
}

class UpdateUser : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "UpdateUser";
    }

    public override void OnRequest()
    {
        System.Console.WriteLine("Updating User!");
        ToolBox.Request(RequestState.Finish);
    }
}
