using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqDefault;
public class RMQ_Recieve : IDisposable
{
    private const string exchangeName = "tweet_exchange_name"; // Scoped as a constant within the class

    public RMQ_Recieve(string connectionString, string routingKey)
    {
        Factory = new ConnectionFactory { Uri = new Uri(connectionString), ClientProvidedName = "provideName" };
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

  public void StartListening(Func<byte[], byte[]> handleMessage)
  {
    string consumerTag = channel.BasicConsume(queue: QueueName, autoAck: false, consumer: Consumer);
    Consumer.Received += (model, arg) =>
    {
      try{

        Sidecar<EncryptionSidecar> sidecar = new Sidecar<EncryptionSidecar>();

        var body = arg.Body.ToArray();
        // byte[] response = sidecar.Get().Decrypt(handleMessage(body));

        if (arg.BasicProperties?.ReplyTo != null){
          var replyProps = channel.CreateBasicProperties();
          replyProps.CorrelationId = arg.BasicProperties.CorrelationId;
          channel.BasicPublish(
            exchange: "",
            routingKey: arg.BasicProperties.ReplyTo,
            basicProperties: replyProps,
            body: handleMessage(body));
        }
        channel.BasicAck(deliveryTag: arg.DeliveryTag, multiple: false);
      }
      catch (Exception ex){
        byte[] errorResponse = Encoding.UTF8.GetBytes($"Error: {ex.Message}");
        if (arg.BasicProperties?.ReplyTo != null){
          var replyProps = channel.CreateBasicProperties();
          replyProps.CorrelationId = arg.BasicProperties.CorrelationId;

          channel.BasicPublish(
            exchange: "",
            routingKey: arg.BasicProperties.ReplyTo, 
            basicProperties: replyProps,
            body: errorResponse);
        }

        channel.BasicAck(deliveryTag: arg.DeliveryTag, multiple: false);
      }
    };
  }



    public void Dispose()
    {
        string consumerTag = channel.BasicConsume(QueueName, false, Consumer);
        channel.BasicCancel(consumerTag);
        channel.Close();
        cnn.Close();
    }

    public string RoutingKey { get; set; }
    public string QueueName { get; set; }

    public EventingBasicConsumer Consumer { get; private set; }
    public ConnectionFactory Factory { get; private set; }
    public IConnection cnn { get; private set; }
    public IModel channel { get; private set; }
}
