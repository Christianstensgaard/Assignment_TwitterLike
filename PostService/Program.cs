using PostService.Service;
using RabbitMQ.Client;

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

          //- Services
          new CreatePost().Start(connectionString, "post.create");



          while(true)
            Console.ReadLine();
        }
    }
}
