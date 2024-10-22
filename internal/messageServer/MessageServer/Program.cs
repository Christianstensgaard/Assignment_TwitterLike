//- Message Server

using System.Net.Sockets;
using BitToolbox;
using MessageServer;


NetworkController network = new NetworkController();
network.Start();
bool Running = true;
while(Running){
  System.Console.WriteLine("Write Command");
  switch(Console.ReadLine()){

    case "exit":
      Running = false;
     break;



    default:
    break;
  }
}
