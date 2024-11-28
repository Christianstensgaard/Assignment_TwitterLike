# Week 48 - Design Patterns
## Objectives
This week is a self-study on microservices design patterns. You should have be now already implemented one, the API gateway. The goal is to learn more about popular microservices design patterns and implement one in your Twitter system.


# Tasks
1. [goto](#1-research) **Research popular microservices design patterns.**
2. [goto](#2-identify) **Identify a design pattern that is a good fit for your Twitter system.**
3. [goto](#3-implement) **Implement the design pattern in your Twitter system.**




## 1. Research

#### Side-car design patten



more general information can be found here 
[Side-car](#side-car)



### 2. Identify
looking for a design patten that match the use-case for my twitter system. there are some key-features i look for.


### 3. Implement






# Design-pattens documentation

## Side-car
**Context** and problem
Applications and services often require related functionality, such as monitoring, logging, configuration, and networking services. These peripheral tasks can be implemented as separate components or services.

If they're tightly integrated into the application, they can run in the same process as the application, making efficient use of shared resources. However, this also means they're not well isolated, and an outage in one of these components can affect other components or the entire application. Also, they usually need to be implemented using the same language as the parent application. As a result, the component and the application have close interdependence on each other.

If the application is decomposed into services, then each service can be built using different languages and technologies. While this gives more flexibility, it means that each component has its own dependencies and requires language-specific libraries to access the underlying platform and any resources shared with the parent application. In addition, deploying these features as separate services can add latency to the application. Managing the code and dependencies for these language-specific interfaces can also add considerable complexity, especially for hosting, deployment, and management.

[learn.microsoft](https://learn.microsoft.com/en-us/azure/architecture/patterns/sidecar)

## Event sourcing
**Context**
A service command typically needs to create/update/delete aggregates in the database and send messages/events to a message broker. For example, a service that participates in a saga needs to update business entities and send messages/events. Similarly, a service that publishes a domain event must update an aggregate and publish an event.

The command must atomically update the database and send messages in order to avoid data inconsistencies and bugs. However, it is not viable to use a traditional distributed transaction (2PC) that spans the database and the message broker The database and/or the message broker might not support 2PC. And even if they do, it’s often undesirable to couple the service to both the database and the message broker.

But without using 2PC, sending a message in the middle of a transaction is not reliable. There’s no guarantee that the transaction will commit. Similarly, if a service sends a message after committing the transaction there’s no guarantee that it won’t crash before sending the message.

In addition, messages must be sent to the message broker in the order they were sent by the service. They must usually be delivered to each consumer in the same order although that’s outside the scope of this pattern. For example, let’s suppose that an aggregate is updated by a series of transactions T1, T2, etc. This transactions might be performed by the same service instance or by different service instances. Each transaction publishes a corresponding event: T1 -> E1, T2 -> E2, etc. Since T1 precedes T2, event E1 must be published before E2.

[microservice.io](https://microservices.io/patterns/data/event-sourcing.html)

## Saga
**Context**
You have applied the Database per Service pattern. Each service has its own database. Some business transactions, however, span multiple service so you need a mechanism to implement transactions that span services. For example, let’s imagine that you are building an e-commerce store where customers have a credit limit. The application must ensure that a new order will not exceed the customer’s credit limit. Since Orders and Customers are in different databases owned by different services the application cannot simply use a local ACID transaction.

[microservices.io](https://microservices.io/patterns/data/saga.html)