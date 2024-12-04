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
    string[] strings = Encoding.UTF8.GetString(stream).Split(',');

    System.Console.WriteLine(strings[0]);
    System.Console.WriteLine(strings[1]);

    return ServiceState.Ok;
  }
}