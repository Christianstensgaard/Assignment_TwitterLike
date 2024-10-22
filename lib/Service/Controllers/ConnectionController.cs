using System.Net.Sockets;

namespace Service.Controllers;
public class ConnectionController{
  public ConnectionController(string address, int port){
    this.address = address;
    this.port = port;

    socket = new TcpClient();
    socketThread = new Thread(BackgroundWorker);
  }

  public void Start(){
    socketThread.Start();
  }
  public void Exit(){
    socket.Close();
  }
  public void Write(byte[] payload){
    if(!socket.Connected)
      return;

    socket.GetStream().Write(payload);
    socket.GetStream().Flush();
  }


  public bool Connected => socket.Connected;


  internal delegate void ClientConnectedDelegate(TcpClient client);
  internal event ClientConnectedDelegate OnClientConnected;

  internal delegate void ConnectedDelegate(TcpClient client);
  internal event ConnectedDelegate OnConnected;


  void OpenConnection(){
    socket.Connect(address, port);

    if(socket.Connected)
      OnConnected?.Invoke(socket);
  }
  void HandleSocketRequest(){
    System.Console.WriteLine("HandleSocketRequest()");
    OnClientConnected?.Invoke(socket);
  }
  void BackgroundWorker(){
    int reconnectCounter = 0;
    bool Running = true;
    while(Running){
      try
      {
        if(!socket.Connected){
          if(reconnectCounter++ < 5){
            OpenConnection();
            if(socket.Connected)
              break;
            else Thread.Sleep(250);

          } else Running = false;
            
        }
        if(socket.Available > 0)
          HandleSocketRequest();
        else Thread.Sleep(250);
      }
      catch (System.Exception)
      {
        System.Console.WriteLine("Log this...");
        if(reconnectCounter > 5)
          throw new Exception("Failed to connect to server");
      }
    }
  }


  //PRIVATE Variables.
  TcpClient socket;
  Thread socketThread;
  string address;
  int port;
}