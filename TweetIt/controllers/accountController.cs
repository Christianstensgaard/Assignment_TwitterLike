using System.Text;
using RabbitMqDefault;

namespace TweetIt.Controllers;
public class AccountController{
  public static string ConnectionString = "amqp://guest:guest@rabbitmq:5672";
  public async void CreateAccount(string username, string hashpass){
    System.Console.WriteLine("CreateAccount()");
    CircuitBreaker circuitBreaker = new CircuitBreaker(4, TimeSpan.FromSeconds(10));
    await circuitBreaker.ExecuteAsync(
      action: async ()=>{
        using (RMQ_Send send = RMQ_Send.Send(RouteNames.Account_create)){
          send.Body = Encoding.UTF8.GetBytes($"{username},{hashpass}");
          string a  = await send.SendAndAwaitResponseAsync(TimeSpan.FromSeconds(2));
          System.Console.WriteLine(a);
        };
      },

      onFallback: ()=>{
        System.Console.WriteLine("Failed to Create account");
      }

    );
  }
}