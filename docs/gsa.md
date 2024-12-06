# System architecture. 

Taking a closer look at the microservices architecture, we can begin to examine the overall system design:

![Insert Image Here](/docs/img/systemarchitecture.png)

The system is divided into smaller services, such as Account, Post, and Notify.

Focusing on the Account service, it manages all account-related operations, including creating, updating, and deleting accounts, as well as login and logout functionalities. The Account service runs independently from the web server within a Docker container. This approach allows the service to scale horizontally (on the X-axis) as demand increases.

The same architectural principles apply to the other services. All services are interconnected via a message broker, which facilitates their communication.

The plan was to make the web server the sole entry point for clients. This design isolates the services, enhancing the system's security. Within the services, all data is encrypted, and the sidecar design pattern is implemented to extend and augment the services.

For scaling, my idea was to duplicate the web server during periods of high load, using it as a gateway to the underlying services. Currently, the system employs UTF-8 encoding to identify service locations. However, this could be optimized by transitioning to a 3-byte hexadecimal identifier, reducing the overhead associated with textual identifiers. Additionally, adopting a static IP range for services could eliminate the need to analyze service headers before making calls.

Regarding encryption, I developed a simple encryption class capable of encrypting streams. The long-term plan is for this class to support automatic key updates (e.g., time-based updates) to ensure the encryption key remains dynamic while the system is operational. On top of this, implementing a network handshake protocol before accepting connections would significantly bolster system security.

In summary, the system comprises a web server that clients connect to. The web server handles HTTP requests, parses them, and invokes the necessary services to fulfill those requests. Given the emphasis on microservices, creating a dedicated service for parsing HTTP requests (e.g., JSON) could enhance scalability and optimize data handling.

Additionally, introducing a service to manage and prioritize different types of requests could further streamline the system's operations.

[Home](/README.md)