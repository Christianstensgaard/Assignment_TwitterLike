using RabbitMQ.Client;

namespace RabbitMqDefault;
public class RMQ_Send : IDisposable{
public RMQ_Send(string connectionString, string routingKey){
  Factory = new();
  Factory.Uri = new Uri(connectionString);
  Factory.ClientProvidedName = "provideName";
  RoutingKey = routingKey;
  QueueName = $"{routingKey}_queue";

  cnn = Factory.CreateConnection();
  channel = cnn.CreateModel();

  channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
  channel.QueueDeclare(QueueName, false, false, false, null);
  channel.QueueBind(QueueName, exchangeName, routingKey, null);
}


public byte[]? Body {get; set;}

const string exchangeName = "tweet_exchange_name";
public string RoutingKey {get; set;}
public string QueueName {get;set;}


public ConnectionFactory Factory {get; private set;}
public IConnection cnn {get; private set;}
public IModel channel {get; private set;}

public void Dispose()
{
  if(Body != null)
    channel.BasicPublish(exchangeName, RoutingKey, null, Body);

  channel.Close();
  cnn.Close();
}
}
