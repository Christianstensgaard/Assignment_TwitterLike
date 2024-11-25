using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using Xunit;

namespace PostService.Tests
{
    public class ProgramTests
    {
        // [Fact]
        // public void WaitForRabbitMQ_Connect()
        // {
        //     var connectionString = "amqp://guest:guest@localhost:5672";
        //     var mockConnection = new Mock<IConnection>();
        //     var mockFactory = new Mock<ConnectionFactory>();
        //     mockFactory.Setup(f => f.CreateConnection()).Returns(mockConnection.Object);

        //     Program.WaitForRabbitMQ(connectionString);
        // }

        // [Fact]
        // public void WaitForRabbitMQ_RetryOnFailure()
        // {
        //     var connectionString = "amqp://guest:guest@localhost:5672";
        //     var mockFactory = new Mock<ConnectionFactory>();
        //     var attempt = 0;

        //     mockFactory
        //         .Setup(f => f.CreateConnection())
        //         .Callback(() =>
        //         {
        //             attempt++;
        //             if (attempt < 3) throw new Exception("Connection failed");
        //         })
        //         .Returns(Mock.Of<IConnection>());

        //     Program.WaitForRabbitMQ(connectionString);
        //     Assert.Equal(3, attempt);
        // }

        // [Fact]
        // public void WaitForRabbitMQThrowExceptionAfterMaxRetries()
        // {
        //     var connectionString = "amqp://guest:guest@localhost:5672";
        //     var mockFactory = new Mock<ConnectionFactory>();
        //     mockFactory
        //         .Setup(f => f.CreateConnection())
        //         .Throws(new Exception("Connection failed"));

        //     var exception = Assert.Throws<Exception>(() =>
        //         Program.WaitForRabbitMQ(connectionString)
        //     );
        //     Assert.Contains("Failed to connect to RabbitMQ", exception.Message);
        // }

        // [Fact]
        // public void ConsumerReceivedMessageCorrectly()
        // {
        //     var connectionString = "amqp://guest:guest@localhost:5672";
        //     var routingKey = "account.create";
        //     var bodyContent = new byte[] { 0xA1 };
        //     var mockChannel = new Mock<IModel>();
        //     var mockConnection = new Mock<IConnection>();
        //     mockConnection.Setup(c => c.CreateModel()).Returns(mockChannel.Object);

        //     var mockFactory = new Mock<ConnectionFactory>();
        //     mockFactory.Setup(f => f.CreateConnection()).Returns(mockConnection.Object);

        //     var mockConsumer = new EventingBasicConsumer(mockChannel.Object);
        //     var eventTriggered = false;

        //     mockConsumer.Received += (sender, args) =>
        //     {
        //         Assert.Equal(bodyContent, args.Body.ToArray());
        //         eventTriggered = true;
        //     };

        //     mockConsumer.HandleBasicDeliver("tag", 1, false, "exchange", "routingKey", Mock.Of<IBasicProperties>(), bodyContent);

        //     Assert.True(eventTriggered);
        // }

        // [Fact]
        // public void ConsumerSkipInvalidMessageBody()
        // {
        //     // Arrange
        //     var connectionString = "amqp://guest:guest@localhost:5672";
        //     var routingKey = "account.create";
        //     var bodyContent = new byte[] { 0xFF }; // Invalid message

        //     var mockChannel = new Mock<IModel>();
        //     var mockConsumer = new EventingBasicConsumer(mockChannel.Object);
        //     var eventTriggered = false;

        //     mockConsumer.Received += (sender, args) =>
        //     {
        //         if (args.Body.ToArray()[0] == 0xA1)
        //         {
        //             eventTriggered = true;
        //         }
        //     };

        //     mockConsumer.HandleBasicDeliver("tag", 1, false, "exchange", "routingKey", Mock.Of<IBasicProperties>(), bodyContent);

        //     Assert.False(eventTriggered);
        // }
    }
}
