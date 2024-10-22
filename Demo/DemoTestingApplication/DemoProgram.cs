using Service;
using Service.Tools;

byte[] t1 = HeaderManager.CreateHeader("AccountService", "CreateAccount");
byte[] t2 = HeaderManager.CreateHeader("AccountService", "CreateAccount");
System.Console.WriteLine("Header Equal test OK:     " + HeaderManager.EqualFunctionName(t1, t2));

PackageBuilder builder = new PackageBuilder(t1);
builder.Pack("Hello World");
builder.Pack("Christian");
builder.Pack("Jørgensen");

builder.Pack(200, 201, 202, 100, 200,200,2002,1);
builder.Pack("Hello world");

//-- PackageReader --
byte [] resultBuffer = builder.PackAndSign();

//test if header is correct placed.
bool result = HeaderManager.EqualServiceName(t1, resultBuffer);
System.Console.WriteLine("Header Test OK:           " + result);

var reader = PackageReader.BeginRead(resultBuffer);
string a = reader.String();
string b = reader.String();
string c = reader.String();

bool testResult = a == "Hello World" && b == "Christian" && c == "Jørgensen";

System.Console.WriteLine("String Test OK:           " + testResult);











Service.Service.Runtime.ServiceName = "AccountService";
Service.Service.Runtime.LocalService = true; //- using it as a local service system only.
Service.Service service = Service.Service.Runtime;
service.AddService(new ServiceA());
service.AddService(new ServiceB());

Service.Service.Runtime.Start();

service.CreateRequest(HeaderManager.CreateHeader("DatabaseService", "ServiceA"), resultBuffer);
service.CreateRequest(HeaderManager.CreateHeader("AccountService", "ServiceB"), resultBuffer);

class ServiceA : AService
{
    public override void OnInit(ServiceConfig config)
    {
      config.ServiceName = "ServiceA";
    }

    public override void OnRequest()
    {
        System.Console.WriteLine("Serice A Called!");
    }
}


class ServiceB : AService
{
    public override void OnInit(ServiceConfig config)
    {
      config.ServiceName = "ServiceB";
    }

    public override void OnRequest()
    {
        System.Console.WriteLine("Serivce B Called!");
    }
}


