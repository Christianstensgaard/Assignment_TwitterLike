using BitToolbox;
using Service.Core;

namespace Service;
public static class ToolBox{
  public static void NewRequest(string ClientName, string functionName, byte[] payload)
  {
    ToolBox.RunTime.AddRequest(new RequestModel(){
      Header = HeaderManager.CreateHeader(ClientName, functionName),
      Payload = payload
    });
    System.Console.WriteLine("Request Added");
  }

  public static void NewLocalRequest(string serviceFunction, byte[] payload){
    ToolBox.RunTime.AddRequest(new RequestModel(){
      Header = HeaderManager.CreateHeader(RunTime.ServiceClientName, serviceFunction),
      Payload = payload
    });
  }

  public static ServiceController RunTime => ServiceController.Runtime;
}


public class RequestModel{
  public byte[] Header { get; set; }
  public byte[] Payload { get; set; }
}