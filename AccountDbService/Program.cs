
using AccountDbService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//- Sender

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672"); // Correct format

factory.ClientProvidedName = "Account Create";

IConnection cnn = factory.CreateConnection();
IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "none";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);


byte[] messageBody = Encoding.UTF8.GetBytes("Hello world");
channel.BasicPublish(exchangeName, routingKey, null, messageBody);








channel.Close();
cnn.Close();


Reciever r = new Reciever();
