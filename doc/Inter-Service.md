# Week 40 - Inter-Service Communication
## Objectives
Implement and demonstrate effective inter-service communication.

# Tasks
1. **Communication Protocol: Select a communication protocol (e.g., REST, RabbitMQ, gRPC) and justify the choice.**
2. **API Gateway Implementation: Establish an API Gateway to manage external and internal service requests.**
3. **Service Interaction: Implement sample functions illustrating communication between services, focusing on security and data 4. integrity.**



## 1. Communication Protocol:
I decided to utilize RabbitMQ as the messaging broker for our services, primarily due to its simplicity and ease of use. The broker's design allows for straightforward communication between services, making it an ideal choice for handling TCP-Socket stream interactions.

Additionally, RabbitMQ offers a convenient way to integrate with Docker, enabling us to containerize the broker and create a self-contained environment. This also provides a bonus advantage: we can leverage RabbitMQ's built-in web interface to manage the broker, monitor its performance, and perform administrative tasks remotely.

[RabbitMQ_sender](/RabbitMqDefault/RMQ_Send.cs)

[RabbitMQ_receiver](/RabbitMqDefault/RMQ_Recieve.cs)

## 2. API Gateway Implementation
I explored alternative approaches to handle the gateway and initially opted for a traditional web server to process requests directly, which then calls RabbitMQ. This straightforward approach seemed suitable since HTTP requests need to be parsed into a different format before being processed by the internal services.

However, I considered an additional layer of processing to further optimize the solution. Specifically, I envisioned creating a separate service dedicated solely to parsing the incoming HTTP request, transforming it into a compatible format, and then passing it on to the relevant internal services.

## 3. Service Interaction
By moving away from direct HTTP requests to the web server and instead leveraging a standalone API, I've significantly improved security. However, there's still room for additional analysis to ensure even more robust security measures.

Given that we're not relying on HTTP to call the services directly, we can now apply more advanced security checks to validate and sanitize data before calling the service. This could include:

1. Input validation to prevent SQL injection or cross-site scripting (XSS) attacks
2. Data normalization and sanitization to remove sensitive information
3. Authentication and authorization checks to ensure only authorized requests reach the service
4. Additional encryption methods, such as SSL/TLS or message authentication codes (MACs), to    further secure data in transit

By taking a more proactive approach to security, we can create a more resilient and trustworthy API that protects both our internal services and external users.

```csharp
// Ensure Action and Payload are provided
if (string.IsNullOrWhiteSpace(RequestModel?.Action) || string.IsNullOrWhiteSpace(RequestModel?.Payload))
{
    ServerResponse = "Error: Action or Payload cannot be empty.";
    return;
}
// Construct the JSON based on input values
var jsonInput = new { action = RequestModel.Action, payload = RequestModel.Payload };
var jsonString = JsonSerializer.Serialize(jsonInput);
```

My idea is that the web server handles the request for the client, which should be changed to a standalone API that handles different requests for internal services.

The requests are analyzed and converted into a more efficient protocol, removing all unnecessary data from the payload, before calling the service. This approach ensures that the bandwidth of the network is minimized.

````csharp
public class AccountController{
  public static string ConnectionString = "amqp://guest:guest@rabbitmq:5672";
  public void CreateAccount(string username, string hashpass){
    using (RMQ_Send send = new RMQ_Send(ConnectionString, "account.create")){
      send.Body = Encoding.UTF8.GetBytes($"{username},{hashpass}");
    };
  }
}
````
>[!NOTE]
> This demonstration serves as a simplified illustration of the Account controller's functionality, which should not be used in production without modification. Specifically, the use of a string-based method for creating a service is deprecated and should be replaced with a more suitable approach.


The Account controller plays a critical role in managing account-related operations by orchestrating data exchange between the application and external services. In this scenario, the controller receives input data and generates a request to create an account, which is subsequently stored within the database.


# Conclusion
By transitioning away from HTTP and adopting a pure TCP Socket stream protocol, we can significantly enhance the security and isolation of our internal services. This approach enables us to encrypt communication between services, effectively shielding them from external threats and minimizing the risk of data breaches.

Furthermore, eliminating HTTP as an intermediary in our service pipeline has a dual benefit: it frees up bandwidth that would otherwise be consumed by HTTP requests, allowing us to support more concurrent users or reduce energy consumption. By offloading the overhead of HTTP, we can allocate those resources towards processing power, further improving overall system performance and efficiency.