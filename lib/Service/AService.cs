namespace Service;
using Service.Models;
public abstract class AService
{
  /// <summary>
  /// When the Service is called this will be called from X
  /// </summary>
  protected abstract void OnRequest();
  /// <summary>
  /// Running once on Start
  /// </summary>
  protected abstract void OnStart(ServiceConfig ServiceSettings);
  /// <summary>
  /// Running once on close
  /// </summary>
  protected abstract void OnClose();



  protected void WriteRequest(){
    // writer request to 
  }


  protected void CreateRequest(){
    //- Create a request to be sendt over the message bus
  }

  //- Storing the Service information.
  RequestModel? requestModel;

}
