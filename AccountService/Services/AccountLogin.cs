using System.Text;
using RabbitMqDefault.interfaces;

namespace AccountService.Services;
public class AccountLogin : AService
{
  public override void onFallback()
  {
    System.Console.WriteLine("Fallback called!");
  }

  public override bool OnInit()
  {
    return true;
  }

  public override ServiceState OnInvoke(byte[] stream)
  {
    //- Using CSV, just for simplicity. 
    /*
      * Log the use in, and return a token or what ever you normally do on a web site.
      * 
      * I have not created a lot of the services, and they are here for demo only.
    */

    return ServiceState.Ok;
  }
}