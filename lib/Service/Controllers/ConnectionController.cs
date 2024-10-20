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
    OpenConnection();
    if(!Connected)
      socketThread.Start();
  }
  public void Exit(){
    socket.Close();
  }

  public bool Connected => socket.Connected;
  
  void OpenConnection(){
    socket.Connect(address, port);
  }
  void HandleSocketRequest(){
    //- Handle connection request...

  }
  void BackgroundWorker(){
    while(true){
      try
      {
        if(!socket.Connected){
          //- Trying to reconnect.
          for (int i = 0; i < 5; i++)
          {
            OpenConnection();
            if(socket.Connected)
              break;
            else Thread.Sleep(250);
          }
        }

        //- Data is avalilable!
        if(socket.Available > 0)
          HandleSocketRequest();
        else Thread.Sleep(250);
      }
      catch (System.Exception)
      {
        System.Console.WriteLine("Log this...");         
      }
    }
  }



  //PRIVATE Variables.
  TcpClient socket;
  Thread socketThread;
  string address;
  int port;
  
}