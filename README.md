# Application Development notes
> [!TIP]
> Generel Application development notes

> [!WARNING]
> 27-10-24
> Made a mistake while creating something in the tracing, and therefore made a mistake so this branch and the messageServer, don't work,
> Please use the commit before this one, if testing the system

> [!NOTE] 
  **Message Server down**
  Right now, there is some issues with the Message Server after a rebase in a merge request. therefore i will be refactoring the Server, to handle different pre tasks before calling the slave. this will include Task system, and callback functions
  


# System Updates
> [!TIP]
> All newly created Updates are displayed here filtred by date.








# Project Documentation
> [!NOTE]
> This is a schoool project, and should not be used for real-life application.
> the only reason this repo is public is for sharing purpose only.

# Document Navigation
1. [Getting Started](#getting-started-using-the-system)
2. [System Design](#system-design)
  1. [Libarary](#libarary)
  2. [Services](#services)
  3. [Message-Server](#message-server)
  4. [Databases](#databases)

# Getting started using the system
1. Install Docker
2. Run Docker
3. Done


# System design
> [!TIP]
> The solution is build up in different componements, to split the task for the system.

## System Types
- a deep-dive into how the system is designed and used.

### Libarary
> [!TIP]
> Alot of the lib's are not optimized and therefore there will be some functions that should be refactored.

1. #### BitToolBox
  1. **BitHandler** this class is used to set a single bit in a Array of bytes, in the future, this can be used to compress different payloads,
     to make the size of the package alot smaller, but right now, this is not used.

  2. **ByteBufferController** During the development of the message system, frequent allocation of various byte arrays on the heap led me to design a global buffer system. This buffer allows for reading and writing data without constantly creating new instances, significantly reducing memory allocations. By minimizing instance creation and removal, this approach alleviates pressure on the Garbage Collector, improving performance in scenarios with high allocation demands.

  3. **HeaderManager** This class is used by the server to manage the location of data packages (service calls) and provides various functions to streamline packing and reading of the header file.

2. #### Service
  The service libarary are the client, for the service client system, and have all the features to Create services including request and response.

### Services
- In this project, I've developed two main services that manage accounts and posts, both designed to be fully scalable across both X and Y dimensions as needed.

Each service operates independently, handling its specific tasks using the serviceFunction class, which connects directly to the message client. The client invokes virtual functions when the services are called, allowing each class to perform its work and either return a "done" status or trigger additional functions in linked services.

Throughout development, a key focus was on building robust tracing and logging within the service client itself, making the message system much easier to use when developing and troubleshooting new services.

> [!IMPORTANT]
> Alot of the Logging and Tracing functions are not working, and there is only a function at the moment, to simulate where the different logging should take place.

### Message-server
- The Message Server... oh god

### Databases
- The databases are connected over using a DatabaseService, connected to the Message server, and can be called by all the connected services.

> [!IMPORTANT] 
> At this point, there are no rules build in, to handle what types of services that can use the Database Serviecs. In the Production this should maybe be changed
> to create Rules on the network.


# Conclusion
1. Message System Design
I put significant effort into designing the message system to handle various requests effectively. This included implementing an initial tracing and logging system, designing asynchronous communication, and using the TCP protocol with minimal package overhead. Creating both the message server and client was a substantial task, involving extensive design and testing. While a text-based communication approach might have been simpler, it would introduce additional overhead, which I aimed to minimize.

2. Service Functionality
Given the time invested in designing the message client and server, the services are not fully functional yet. Although that may be disappointing, they are operational and callable, allowing the overall system flow to be demonstrated, even if it doesn’t yet perform every function as intended.


3. Project Reflection
Overall, this project was an enjoyable experience, as I explored creating services using a microservices architecture. I gained a lot of valuable knowledge throughout the process, so I consider the project a success.
