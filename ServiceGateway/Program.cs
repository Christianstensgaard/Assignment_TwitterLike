string connectionString = "amqp://guest:guest@rabbitmq:5672";
RabbitMqDefault.RMQ_Recieve rMQ_Recieve = new RabbitMqDefault.RMQ_Recieve(connectionString, "api.builder");

rMQ_Recieve.Consumer.Received += (sender, arg) =>{
  byte[] budy = arg.Body.ToArray();
};