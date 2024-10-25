>[INFO]
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
>[!TIP]
> The solution is build up in different componements, to split the task for the system.

## System Types
- a deep-dive into how the system is designed and used.

### Libarary
>[!Tip]
> Alot of the lib's are not optimized and therefore there will be some functions that should be refactored.

1. #### BitToolBox
  1. **BitHandler** this class is used to set a single bit in a Array of bytes, in the future, this can be used to compress different payloads, 
     to make the size of the package alot smaller, but right now, this is not used. 

  2. **ByteBufferController** under the development of the message system, allocation alot of different byte arrys on the heap all time, made me create a 
     system that has a global buffer, that can be used to Read and Write data to instead of creating all the different instances, this is made to hande alot of 
     allocation, and reduce the stress on the GarbageCollector if alot of instances are created and removed.

  3. **HeaderManager** this class is used by the  Server, to manage the location of the data package, (service call), and have different functions
     to easy Pack and Read the header file.

2. #### Service
  The service libarary are the client, for the service client system, and have all the features to Create services including request and response. 

### Services
- in this project i have created two main services, that can handle account and post, all the functions are fully scaleable on both X and Y 
  if needed.

  all the different services don't know eachother, and handle the single task they are assigned to using the serviceFunction class. that are 
  directly connected to the message-client. the client will call the virtual function when the services are called, and the class can do some work, and return done, 
  or call other functions in the linked services.

  doing the development, Tracing and Logging was one of the goals, and i wanted to build it inside the Service client, so using the messageSystem will be alot easyer, doing 
  development of the services. 

  >[!NOTE]
  > Alot of the Logging and Tracing functions are not working, and there is only a function at the moment, to simulate where the different logging should take place.

### Message-server
- The Message Server... oh god

### Databases
- The databases are connected over using a DatabaseService, connected to the Message server, and can be called by all the connected services.

>[!INFO] At this point, there are no rules build in, to handle what types of services that can use the Database Serviecs. In the Production this should maybe be changed 
> to create Rules on the network.


# Conclusion
1. I have been using alot of energy on designing the message system, to handle the different requests. this includes
   Beginning of Tracing and Logging system, async communication desing, Tcp protocol, with minimal package overhead.
   Creating the message server and client was a big task, that took alot of my time designing and testing. and maybe
   i should had created something more txt based communication, since its alot simpler. but that will create overhead,
   that i wanted to handle. 

2. Given the time i took to design the msg client and server, the services are not fully functional, and that might be
   sad but given that they are there and can be called, the idea stays the same, and the flow of the system can be seen, even
   if the system don't do the right things.


3. overall this project was alot of fun, trying to create different services that use the microservices architecture. 
   and i learned alot doing the process, so i would see this as a succsess. 
