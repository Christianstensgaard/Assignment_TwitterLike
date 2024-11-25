using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqDefault;

public class RMQ_Send : IDisposable
{
    public static RMQ_Send Send(string routingKey)
    {
        return new RMQ_Send(Connect.ConnectionString, routingKey);
    }

    public RMQ_Send(string connectionString, string routingKey)
    {
        Factory = new ConnectionFactory { Uri = new Uri(connectionString), ClientProvidedName = "provideName" };
        RoutingKey = routingKey;
        QueueName = $"{routingKey}_queue";

        cnn = Factory.CreateConnection();
        channel = cnn.CreateModel();

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(QueueName, false, false, false, null);
        channel.QueueBind(QueueName, exchangeName, routingKey, null);
    }

    public byte[]? Body { get; set; }

    private const string exchangeName = "tweet_exchange_name";
    public string RoutingKey { get; set; }
    public string QueueName { get; set; }

    public ConnectionFactory Factory { get; private set; }
    public IConnection cnn { get; private set; }
    public IModel channel { get; private set; }

    public async Task<string> SendAndAwaitResponseAsync(TimeSpan timeout)
    {
      var replyQueueName = channel.QueueDeclare().QueueName;
      var consumer = new EventingBasicConsumer(channel);

      var tcs = new TaskCompletionSource<string>();

      var correlationId = Guid.NewGuid().ToString();

      consumer.Received += (sender, args) =>
      {
          var response = Encoding.UTF8.GetString(args.Body.ToArray());
          if (args.BasicProperties.CorrelationId == correlationId)
          {
              tcs.TrySetResult(response);
          }
      };

      channel.BasicConsume(replyQueueName, true, consumer);

      var props = channel.CreateBasicProperties();
      props.CorrelationId = correlationId;
      props.ReplyTo = replyQueueName;

      channel.BasicPublish(exchangeName, RoutingKey, props, Body);

      using var cts = new CancellationTokenSource(timeout);
      cts.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

      try
      {
          return await tcs.Task;
      }
      catch (TaskCanceledException)
      {
          throw new TimeoutException("The request timed out waiting for a response.");
      }
    }

    public void Dispose()
    {
        channel.Close();
        cnn.Close();
    }
}
