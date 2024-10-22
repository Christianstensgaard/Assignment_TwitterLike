using System.Net.Sockets;
using Service.Controllers;
using Service.Tools;

namespace Service;
public class Service{
  public static Service Runtime {get; private set;} = new Service();
  Service(){
    Services = new LinkedList<AService>();

    #if DEBUG
    #else
      address = Environment.GetEnvironmentVariable("MsgAddress");
      port    = int.Parse(Environment.GetEnvironmentVariable("MsgPort"));
    #endif



    socket = new ConnectionController(address, port);
    socket.OnClientConnected += SocketHandler;
  }

  public delegate void ErrorDelegate();
  public event ErrorDelegate? OnError;
  public string ServiceName { get; set; } = "Undefined";
  public bool LocalService  {get; set;  } = false;
  public Thread? RuntimeThread { get; private set; } = null;
  public void Start(){
    ServiceInit();
    if(!LocalService)
      socket.Start();
  }

  public void AddService(AService service){
    Services.AddLast(service);
  }

  public void CreateRequest(byte[]? header, byte[] payload){
    if(header == null)
      return;
    //- Check if this is a local request.
    if(HeaderManager.EqualServiceName(header, this.header)){
      foreach (var Serivce in Services)
      {
        if(Serivce.ServiceHeader == null)
          continue;
        if(HeaderManager.EqualFunctionName(header, Serivce.ServiceHeader)){
          Serivce.OnRequest();
        }
      }
    }else{
      System.Console.WriteLine("Sending Request over the network");
    }
  }

  string? address = "127.0.0.1";
  int port = 20200;
  byte[] header;


  LinkedList<AService> Services;
  ConnectionController socket;

  void ServiceInit(){
    header = HeaderManager.CreateHeader(ServiceName, "MASTER");
    foreach (var service in Services)
    {
      ServiceConfig config = new ServiceConfig();
      service.OnInit(config);
      service.ServiceHeader = HeaderManager.CreateHeader(ServiceName, config.ServiceName);
    }
  }
  void SocketHandler(TcpClient client){
    byte[] networkBuffer = new byte[1024];
    int size = client.GetStream().Read(networkBuffer);

    if(size <= 0)
      return;


    //Handle the data. and invoke sub classes.
    if(HeaderManager.EqualServiceName(networkBuffer, header)){
      //- This mean the message function call is local, and network can be ignored.
      InvokeLocalRequest();
    }


    //- Else we pack the data, and send it over the network stream.









  }
  
  void InvokeLocalRequest(){

  }
}