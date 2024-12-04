using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using RabbitMqDefault;

namespace TweetIt.Controllers;



public class AccountController{
  public static string ConnectionString = "amqp://guest:guest@rabbitmq:5672";
  public async void CreateAccount(string username, string hashpass){
    using (RMQ_Send send = new RMQ_Send(ConnectionString, RouteNames.Account_create)){
      send.Body = Encoding.UTF8.GetBytes($"{username},{hashpass}");

      string a  = await send.SendAndAwaitResponseAsync(TimeSpan.FromSeconds(2));

      System.Console.WriteLine(a);
    };
  }
}