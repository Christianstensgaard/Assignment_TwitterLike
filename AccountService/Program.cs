using AccountService.Services;
using RabbitMQ.Client;
using RabbitMqDefault;

namespace PostService;
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
              using var connection = factory.CreateConnection();
              Console.WriteLine("RabbitMQ is up and running!");
              return;
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
    WaitForRabbitMQ(connectionString); //- waiting for the service to be connected 

    //- Services
    new CreateAccount().Start(connectionString, RouteNames.Account_create);
    new AccountLogin().Start(connectionString, RouteNames.Account_login);
    new AccountValidation().Start(connectionString, RouteNames.Account_validate_new);

    while(true)
      Console.ReadLine();
  }
}