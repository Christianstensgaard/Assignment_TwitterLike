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
    System.Console.WriteLine("Service Connected: " + client.Client.RemoteEndPoint.AddressFamily);
    if(client.Available > 0){
      System.Console.WriteLine("Data waiting to be read");

      byte[] buffer = new byte[2048];
      int size = client.GetStream().Read(buffer);

      switch(buffer[0]){
        case 0xee:
          if(buffer[1] == 0xff){
            AddService(HeaderManager.ExstractHeader(buffer), client);
          }
        break;

        case 0x01:
          if(buffer[1] == 0xff)
            InvokeService(buffer, size);
        break;

        default:
          System.Console.WriteLine("Package blueprint incorrect!");
        break;
      }
    }
  }
  class ServiceModel{
    public byte[] Header { get; set; }
    public TcpClient socket {get; set;}
  }

  void AddService(byte[] header, TcpClient client){
    System.Console.WriteLine("Adding New Serice to List");
    AddService(new ServiceModel(){
      Header = header,
      socket = client
    });
  }

  void InvokeService(byte[] header, int size){
    System.Console.WriteLine("Invoking Service!");

    System.Console.WriteLine(size);
    foreach (var Service in ServiceModels)
    {
      if(HeaderManager.EqualServiceName(header, Service.Header))
      {
        System.Console.WriteLine("Service Client Found");
      }
    }
  }

  void ServiceRunner(){
    while(true){
      ServiceModel? handle = GetService();

      if(handle != null){
        System.Console.WriteLine("Clit Waiting to handled!");
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

  void HandleRequest(ServiceModel model){
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