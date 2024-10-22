using BitToolbox;
using Service;
using Service.Core;



ToolBox.RunTime.AddService(new ServiceA());
ToolBox.RunTime.AddService(new ServiceB());

ToolBox.RunTime.ServiceClientName = "AccountService";
if(!ServiceController.Runtime.Start("127.0.0.1", 20200))
    return;


ToolBox.NewRequest("AccountService", "ServiceB", new byte[1]);


class ServiceA : ServiceFunction
{
    public override void OnInit(FunctionConfig config)
    {
        config.FunctionName = "ServiceA";
    }


    public override void OnRequest()
    {
        System.Console.WriteLine("ServiceA Called!!!!!!!!!!!!!!!!!");
        ToolBox.NewRequest("Database", "CreateUser", new byte[299]);
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
        ToolBox.NewLocalRequest("ServiceA", new byte[20]);
    }

}