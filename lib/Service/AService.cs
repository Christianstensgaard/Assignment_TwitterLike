using System.Runtime.CompilerServices;
using Service.Interfaces;

namespace Service;
public abstract class AService
{

  protected delegate void ErrorDelegate();
  protected delegate void CloseDelegate();

  protected event ErrorDelegate? OnError;
  protected event CloseDelegate? OnClose;

  public abstract void OnInit(ServiceConfig config);
  public abstract void OnRequest();


  /// <summary>
  /// Write request to Service
  /// </summary>
  /// <param name="streamModel"></param>
  public void Write(IServiceStream streamModel){
    Service.Runtime.CreateRequest(ServiceHeader, streamModel.Serialize());
  }

  /// <summary>
  /// read request, using the IServiceStream ad template
  /// </summary>
  /// <param name="streamModel"></param>
  public void Read(IServiceStream streamModel){
    //TODO Missing logic for this function, and should maybe be changed.
  }

  public string ServiceClientName {get; set;} = "undefined";
  internal byte[]? ServiceHeader;
}

public enum ServiceType{
  Slave,
  Master,
  Hybrid
}

public class ServiceConfig{
  public string ServiceName { get; set; } = "Undefined";
  public ServiceType? ServiceType {get; set;}
}
