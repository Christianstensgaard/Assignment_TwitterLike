// Handle API Gateway. 
using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener socket = new TcpListener(IPAddress.Any, 5000);
socket.Start();

while(true){
  if(socket.Pending()){
    new ConnectionController(socket.AcceptTcpClient()).start();
  } else Thread.Sleep(200);
}

class ConnectionController{
  public ConnectionController(TcpClient client){
    this.client = client;
  }
  public void start(){
    new Thread(BackgroundWorker).Start();
  }

  void BackgroundWorker(){
    string response = string.Empty;
    while(client.Connected){
      try
      {
        if(client.Available > 0){
          StreamReader reader = new StreamReader(client.GetStream());
          string? request = reader.ReadLine();

          if(request != null)
            response = handleUserRequest(request);


        }
        client.GetStream().Write(Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nContent-Length: 13\r\nConnection: close\r\n\r\n{response}"));
        client.Close();
      }
      catch (System.Exception)
      {
      }
    }
  }


  string handleUserRequest(string request){
    System.Console.WriteLine(request);

    string[] filter = request.Split(' ');

    return $"Handled {filter[1]}";
  }

  readonly TcpClient client;
}
