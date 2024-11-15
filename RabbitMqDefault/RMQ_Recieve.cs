using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqDefault;
public class RMQ_Recieve : IDisposable
{
  public RMQ_Recieve(string connectionString, string routingKey){
    Factory = new();
    Factory.Uri = new Uri(connectionString);
    Factory.ClientProvidedName = "provideName";
    RoutingKey = routingKey;
    QueueName = $"{routingKey}_queue";


    cnn = Factory.CreateConnection();
    channel = cnn.CreateModel();

    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
    channel.QueueDeclare(QueueName, false, false, false, null);
    channel.QueueBind(QueueName, exchangeName, RoutingKey, null);
    channel.BasicQos(0, 1, false);

    Consumer = new EventingBasicConsumer(channel);

  }



  public void Dispose(){
    string consumerTag = channel.BasicConsume(QueueName, false, Consumer);
    channel.BasicCancel(consumerTag);
    channel.Close();
    cnn.Close();
  }

  public void StartListening()
    {
        // Start consuming messages
        string consumerTag = channel.BasicConsume(queue: QueueName, autoAck: false, consumer: Consumer);

        // Hook up an event handler (define this externally when creating the consumer)
        Consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);

            Console.WriteLine($"[Consumer] Received: {message}");
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); // Acknowledge the message
        };
    }


  const string exchangeName = "tweet_exchange_name";
  public string RoutingKey {get; set;}
  public string QueueName {get;set;}

  public EventingBasicConsumer Consumer {get; private set;}
  public ConnectionFactory Factory {get; private set;}
  public IConnection cnn {get; private set;}
  public IModel channel {get; private set;}
}
