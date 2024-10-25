using Service;

ToolBox.RunTime.ServiceClientName = "PostService";
ToolBox.AddService(new ValidatePost());
ToolBox.AddService(new CreatePost());
ToolBox.AddService(new UpdatePost());
ToolBox.AddService(new DeletePost());
ToolBox.RunTime.Start("127.0.0.1", 20200);



public class ValidatePost : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "ValidatePost";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("ValidatePost!");
    }
}

public class CreatePost : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "CreatePost";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Creating Post!");
    }
}


public class UpdatePost : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
      config.FunctionName = "UpdatePost";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Updating post!");
    }
}

public class DeletePost : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "DeletePost";
    }

    public override void OnRequest()
    {
      System.Console.WriteLine("Deleting post");
    }
}