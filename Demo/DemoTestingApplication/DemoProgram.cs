using Service;
using Service.Core;

ToolBox.RunTime.ServiceClientName = "API";
ToolBox.AddService(new ServiceA());
ToolBox.AddService(new ServiceB());

if(!ServiceController.Runtime.Start("127.0.0.1", 20200))
    return;


ToolBox.NewRequest("API", "ServiceB", [0xff,0xff]);

class ServiceA : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "ServiceA";
    }

    public override void OnRequest()
    {
        Request("ServiceB", [0xff]);
    }
}

class ServiceB : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "ServiceB";
    }

    public override void OnRequest()
    {
        System.Console.WriteLine("Service B Called");
    }
}