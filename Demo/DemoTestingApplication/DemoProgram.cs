
using System.Net;
using System.Net.Sockets;
using Service.Controllers;

TcpListener boilerSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), 20200);
boilerSocket.Start();
new Thread(ServerSocket).Start();

ConnectionController clientConnection = new ConnectionController("127.0.0.1", 20200);
clientConnection.Start();
System.Console.WriteLine(clientConnection.Connected);







while(true){
  Thread.Sleep(2000);
}



//- ServerSocket Thread function.
void ServerSocket(){
  while(true){
    if(boilerSocket.Pending())
      System.Console.WriteLine("Connection is waiting!");
    else Thread.Sleep(200);
    System.Console.WriteLine("ServerSocker()");
  }
}