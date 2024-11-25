using RabbitMQ.Client;
using Moq;
using Xunit;

namespace RabbitMqDefault.Tests;

public class RMQ_SendTests
{
    private readonly Mock<IConnection> _mockConnection;
    private readonly Mock<IModel> _mockChannel;
    private readonly Mock<ConnectionFactory> _mockFactory;

    public RMQ_SendTests()
    {
        _mockConnection = new Mock<IConnection>();
        _mockChannel = new Mock<IModel>();
        _mockFactory = new Mock<ConnectionFactory>();

        // Mock the connection and channel creation
        _mockConnection.Setup(c => c.CreateModel()).Returns(_mockChannel.Object);
        _mockFactory.Setup(f => f.CreateConnection()).Returns(_mockConnection.Object);
    }

    [Fact]
    public void Constructor_Properties()
    {
        var connectionString = "amqp://localhost";
        var routingKey = "test_key";

        var rmqSend = new RMQ_Send(connectionString, routingKey);

        Assert.Equal(routingKey, rmqSend.RoutingKey);
        Assert.Equal($"{routingKey}_queue", rmqSend.QueueName);
        Assert.NotNull(rmqSend.Factory);
        Assert.NotNull(rmqSend.cnn);
        Assert.NotNull(rmqSend.channel);

        rmqSend.Dispose(); // Clean up
    }

    [Fact]
    public void Dispose_If_Body_Is_Not_Null()
    {
        var connectionString = "amqp://localhost";
        var routingKey = "test_key";
        var body = new byte[] { 0x01, 0x02, 0x03 };

        var rmqSend = new RMQ_Send(connectionString, routingKey)
        {
            Body = body
        };

        rmqSend.Dispose();

        _mockChannel.Verify(
            c => c.BasicPublish(
                It.Is<string>(s => s == "tweet_exchange_name"),
                It.Is<string>(s => s == routingKey),
                null,
                It.Is<byte[]>(b => b == body)
            ),
            Times.Once
        );

        _mockChannel.Verify(c => c.Close(), Times.Once);
        _mockConnection.Verify(c => c.Close(), Times.Once);
    }

    [Fact]
    public void Dispose_Body_Is_Null()
    {
        var connectionString = "amqp://localhost";
        var routingKey = "test_key";

        var rmqSend = new RMQ_Send(connectionString, routingKey);

        rmqSend.Dispose();

        _mockChannel.Verify(
            c => c.BasicPublish(It.IsAny<string>(), It.IsAny<string>(), null, It.IsAny<byte[]>()),
            Times.Never
        );

        _mockChannel.Verify(c => c.Close(), Times.Once);
        _mockConnection.Verify(c => c.Close(), Times.Once);
    }
}
