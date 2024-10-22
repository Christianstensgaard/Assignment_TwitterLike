using System.Net;
using System.Net.Sockets;

namespace MessageServer;
public class NetworkController{
  public NetworkController(){
    Service = new ServiceController();

    string address;
    string port;


    #if DEBUG
      address = "127.0.0.1";
      port = "20200";
    #else   
      port = Environment.GetEnvironmentVariable("MsgPort");
      address = Environment.GetEnvironmentVariable("MsgAddress");
    #endif

    Socket = new TcpListener(IPAddress.Parse(address), int.Parse(port));
  }

  public void Start(){
    Running = true;
    new Thread(NetworkBackgroundRunner).Start();
  }
  public bool Running { get; set; }

  void NetworkBackgroundRunner(){
    Socket.Start();

    System.Console.WriteLine("Message Server Started!");
    while(Running){
      if(Socket.Pending()){
        System.Console.WriteLine("Accepting TcpClient");
        Service.HandleRequest(Socket.AcceptTcpClient());
      } else Thread.Sleep(250);
    }
  }
  ServiceController Service {get; set;}
  TcpListener Socket {get; set;}
}