using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace AccountDbService;
public class Reciever{
  public Reciever(){
    


    ConnectionFactory factory = new();
    factory.Uri = new Uri("amqp://guest:guest@localhost:5672");

    factory.ClientProvidedName = "Account Create";

    IConnection cnn = factory.CreateConnection();
    IModel channel = cnn.CreateModel();

    string exchangeName = "DemoExchange";
    string routingKey = "demo-reouting-key";
    string queueName = "DemoQueue";


    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
    channel.QueueDeclare(queueName, false, false, false, null);
    channel.QueueBind(queueName, exchangeName, routingKey, null);

    channel.BasicQos(0, 1, false);



    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (sender, arg) => {
      
      var body = arg.Body.ToArray();
      string message = Encoding.UTF8.GetString(body);
      System.Console.WriteLine(message);
      channel.BasicAck(arg.DeliveryTag, false);
    };

    string consumerTag = channel.BasicConsume(queueName, false, consumer);

    channel.BasicCancel(consumerTag);
    channel.Close();
    cnn.Close();
  }
}