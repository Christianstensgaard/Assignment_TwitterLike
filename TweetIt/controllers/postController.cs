using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using RabbitMqDefault;

namespace TweetIt.Controllers;



public class PostController{
  public static string ConnectionString = "amqp://guest:guest@rabbitmq:5672";
  public void Createpost(string userid, string postTitle, string postMessage){
    using (RMQ_Send send = new RMQ_Send(ConnectionString, "post.create")){
      send.Body = Encoding.UTF8.GetBytes($"{userid},{postTitle},{postMessage}");
    };
  }
}