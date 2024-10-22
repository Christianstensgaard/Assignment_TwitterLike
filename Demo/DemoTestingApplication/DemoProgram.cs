using BitToolbox;
using Service;

System.Console.WriteLine("Starting Demo Program");


Service.Service service = Service.Service.Runtime;
service.ServiceName = "AccountService";
service.AddService(new CreateAccount());
service.Start();




Thread.Sleep(5000);

service.CreateRequest(HeaderManager.CreateHeader(0xee, "AccountDemo", "FunctionA"), new byte[0]);
service.CreateRequest(HeaderManager.CreateHeader("AccountDemo", "FunctionA" ), new byte[10]);


while(true);

class CreateAccount : AService
{
    public override void OnInit(ServiceConfig config)
    {
        config.ServiceName = "CreateAccount";
    }

    public override void OnRequest()
    {
        System.Console.WriteLine("Request Invoked!");
    }

}