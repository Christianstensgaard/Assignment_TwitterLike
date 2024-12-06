using RabbitMqDefault.interfaces;

namespace AccountService.Services;
public class AccountValidation : AService
{
  public override void onFallback()
  {
    
  }

  public override bool OnInit()
  {
    return true;
  }

  public override ServiceState OnInvoke(byte[] stream)
  {
    return ServiceState.Ok;

    /*
      * Validate the user, checking if the mail / username is already in use. 
      * 
    */
  }
}