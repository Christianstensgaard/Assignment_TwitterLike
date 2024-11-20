using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqDefault;
using System;
using System.Text;
using System.Threading;

namespace PostService
{
    class Program
    {
        public static void WaitForRabbitMQ(string connectionString)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
            int retryCount = 0;

            while (retryCount < 5)
            {
                try
                {
                    using (var connection = factory.CreateConnection())
                    {
                        Console.WriteLine("RabbitMQ is up and running!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    retryCount++;
                    Console.WriteLine($"Retry {retryCount}: Could not connect to RabbitMQ. Error: {ex.Message}");
                    Thread.Sleep(5000); // Wait for 5 seconds before retrying
                }
            }

            throw new Exception("Failed to connect to RabbitMQ after several attempts.");
        }

        static void Main(string[] args)
        {
            string connectionString = "amqp://guest:guest@rabbitmq:5672";
            WaitForRabbitMQ(connectionString);

            MySqlDatabase db_connection = new MySqlDatabase("postuser", "postpassword","post-db", "PostServiceDb");


            // Create consumers and subscribe to the events
            RMQ_Recieve post_service = new RMQ_Recieve(connectionString, "account.create");
            post_service.Consumer.Received += (sender, arg) =>
            {
              byte[] body = arg.Body.ToArray();
              if(body[0] != 0xA1)
                return;

              //TODO fix the converter class before finishing this.
              var tableName = "Accounts";
              var columns = new Dictionary<string, object>
              {
                  { "Username", "testuser" },
                  { "Password", "hashed_password" },
                  { "Activity", 0 }
              };

              string insertQuery = db_connection.CreateInsertQuery(tableName, columns);
            };
            post_service.StartListening();

            RMQ_Recieve delete_service = new RMQ_Recieve(connectionString, "account.update");
            delete_service.Consumer.Received += (sender, arg) =>
            {
                var body = arg.Body.ToArray();
                string output = Encoding.ASCII.GetString(body);
                Console.WriteLine("!:" + output);
            };
            delete_service.StartListening();

            RMQ_Recieve update_service = new RMQ_Recieve(connectionString, "account.delete");
            update_service.Consumer.Received += (sender, arg) =>
            {
                var body = arg.Body.ToArray();
                string output = Encoding.ASCII.GetString(body);
                Console.WriteLine("!:" + output);
            };
            update_service.StartListening();

            RMQ_Recieve get_services = new RMQ_Recieve(connectionString, "account.get");
            update_service.Consumer.Received += (sender, arg) =>
            {
                var body = arg.Body.ToArray();
                string output = Encoding.ASCII.GetString(body);
                Console.WriteLine("!:" + output);
            };
            get_services.StartListening();

            while(true)
                Console.ReadLine();
        }
    }
}
