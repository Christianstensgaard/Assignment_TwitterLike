using System.Net.Sockets;
using BitToolbox;

namespace Service.Core;
public class ServiceController{
  static readonly object _lock = new();
  static object _lock_service = new();
  public static ServiceController Runtime {get; private set;} = new ServiceController();

  public string ServiceClientName { get; set; } = "Undefined";

  public void AddRequest(RequestModel model){
    lock(_lock){
      if(requestPosition >= 9)
      return;
      Requests[requestPosition++] = model;
    }
  }

  public void AddService(ServiceFunction serviceFunction)
  {
    lock(_lock_service){
      Subscribers.Add(serviceFunction);
    }
  }



  public bool Start(string address, int port){
    if(!openConnection(address, port)){
      System.Console.WriteLine("Unable to Connect to Message Server!");
      return false;
    }

    if(!SubscribeAll()){
      System.Console.WriteLine("Error under subscribing all functions to server");
      return false;
    }

    MasterHeader = HeaderManager.CreateHeader(ServiceClientName, "MASTER");

    System.Console.WriteLine("Connected to BusServer");
    running = true;
    tService.Start();
    return true;
  }

  //------------------------------------------------------------------
  //- PRIVATE

  ServiceController(){
    socket = new TcpClient();
    tService = new Thread(BackgroundRunner);
    Requests = new RequestModel[10];
    Subscribers = new List<ServiceFunction>();
    requestPosition = 0;
  }

  TcpClient socket;
  Thread tService;
  bool running = false;
  byte[] MasterHeader;

  RequestModel[] Requests {get; set;}
  List<ServiceFunction> Subscribers {get; set;}
  int requestPosition;


  bool WriteAndFlush(byte[] stream){
    if(!socket.Connected)
      return false;
    socket.GetStream().Write(stream);
    socket.GetStream().Flush();
    return true;
  }

  void BackgroundRunner(){
    byte[] requestBuffer = new byte[1024];
    while(running){
      try
      {
        lock(_lock){
          for (int i = 0; i < requestPosition; i++)
          {
            if(HeaderManager.EqualServiceName(Requests[i].Header, MasterHeader)){
              InvokeSubscribers(PackageManager.Pack(Requests[i].Header, Requests[i].Payload));
              continue;
            }

            socket.GetStream().Write(PackageManager.Pack(Requests[i].Header, Requests[i].Payload));
            socket.GetStream().Flush();
            System.Console.WriteLine("Request Sendt to the Server");
          }
          requestPosition = 0;
        }

        if(socket.Available > 0){
          System.Console.WriteLine("Response Awaiting");
          int size = socket.GetStream().Read(requestBuffer);

          if(HeaderManager.EqualServiceName(requestBuffer, HeaderManager.CreateHeader(ServiceClientName, "Master"))){
            InvokeSubscribers(requestBuffer);
          }

        }
      }

      //- Handle Error as needed.
      catch (System.Exception)
      {

      }
    }
  }
  bool openConnection(string address, int port){
    for (int i = 0; i < 5; i++)
    {
      try
      {
        socket.Connect(address, port);
        return true;
      }
      catch (System.Exception){
        Thread.Sleep(300);
      }
    }
    return false;
  }
  bool SubscribeAll(){
    lock(_lock_service)
    foreach (var item in Subscribers)
    {
      item.OnInit(item.Settings);

      item.FunctionHeader = HeaderManager.CreateHeader(item.ServiceClientName, item.SerivceFunctionName);
      System.Console.WriteLine(item.SerivceFunctionName);
      System.Console.WriteLine(item.ServiceClientName);
       WriteAndFlush(HeaderManager.CreateHeader(0xee, item.ServiceClientName, item.SerivceFunctionName));
    }
    return true;
  }

  void InvokeSubscribers(byte[] stream){
    foreach (var item in Subscribers)
    {
      if(HeaderManager.EqualFunctionName(stream, item.FunctionHeader)){
        item.OnRequest();
        break;
      }
    }
  }


}