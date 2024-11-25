namespace ServiceMonitoring
{
  class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service Monitoring...");

            var monitoringSystem = new ServiceMonitoring();
            monitoringSystem.RegisterService("Service1", "PublicKey123");

            string serviceKey = "Service1";
            string payload = "Sensitive Data";

            Console.WriteLine("\nClient Request:");
            if (monitoringSystem.InvokeService(serviceKey, payload))
            {
                Console.WriteLine("Service call succeeded!");
            }
            else
            {
                Console.WriteLine("Service call failed: Unauthorized access.");
            }
        }
    }
}
