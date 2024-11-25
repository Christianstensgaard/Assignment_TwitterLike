 var consumer = new RMQ_Recieve(connectionString, RouteNames.Account_validate_new);
  consumer.StartListening((message) =>
  {

      // Do some work with the message
      Console.WriteLine($"Processing message: {message}");
      // Return a response back to the sender
    if(message[0] == 0xdd)
      throw new Exception();


      return [0xff];
  });

public void StartListening(Func<byte[], byte[]> handleMessage)
{
  string consumerTag = channel.BasicConsume(queue: QueueName, autoAck: false, consumer: Consumer);
  Consumer.Received += (model, arg) =>
  {
    try{
      var body = arg.Body.ToArray();
      byte[] response = handleMessage(body);

      if (arg.BasicProperties?.ReplyTo != null){
        var replyProps = channel.CreateBasicProperties();
        replyProps.CorrelationId = arg.BasicProperties.CorrelationId;
        channel.BasicPublish(
          exchange: "",
          routingKey: arg.BasicProperties.ReplyTo,
          basicProperties: replyProps,
          body: response);
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