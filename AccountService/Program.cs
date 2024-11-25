﻿using RabbitMQ.Client;
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


      while(true)
        Console.ReadLine();
    }
  }
}
