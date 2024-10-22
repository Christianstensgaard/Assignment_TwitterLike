using System.Net.Sockets;
using BitToolbox;
using MessageServer.Tools;

public class ServiceController{
  static object _lock = new object();
  public ServiceController(){
    ServiceModels = new List<ServiceModel>();
    bufferController = new ByteBufferController();
    new Thread(ServiceRunner).Start();
  }
  public void HandleRequest(TcpClient client){
    System.Console.WriteLine("Service Connected: ");
    AddService(new ServiceModel(){
      Header = [0],
      socket = client
    });
  }
  class ServiceModel{
    public byte[] Header { get; set; }
    public TcpClient socket {get; set;}
  }

  void AddServiceFromStream(byte[] header, TcpClient client){
    ServiceModels.Add(new ServiceModel(){
      Header = header,
      socket = client
    });
  }

  

  void ServiceRunner(){
    while(true){
      ServiceModel? handle = GetService();

      if(handle != null){
        HandleRequest(handle);
      }else Thread.Sleep(250);
    }
  }

  void AddService(ServiceModel model){
    lock(_lock){
      ServiceModels.Add(model);
    }
  }

  ServiceModel? GetService(){
    lock(_lock){
      foreach (var item in ServiceModels)
      {
        if(item.socket != null && item.socket.Available > 0)
          return item;
      }
    }
    return null;
  }

  void NewSubscriber(ServiceModel model){
    var buffer = bufferController.ReadNetworkStream(model.socket.GetStream());
    if(buffer[0] != 0xee)
      return;
    model.Header = HeaderManager.ExstractHeader(buffer.ToArray());
  }

  void HandleRequest(ServiceModel model){

    if(model.Header.Length <= 1){
      NewSubscriber(model);
      return;
    }

    System.Console.WriteLine("Looking for Service Match.");
    var buffer = bufferController.ReadNetworkStream(model.socket.GetStream());
    lock(_lock){
      foreach (var item in ServiceModels)
      {
        if(HeaderManager.EqualServiceName(buffer.Stream, buffer.Start, item.Header)){
          model.socket.GetStream().Write(buffer.Stream, buffer.Start, buffer.Length);
          model.socket.GetStream().Flush();
        }
      }
    }
  }

  ByteBufferController bufferController;
  List<ServiceModel> ServiceModels;
}