# Week 44 - API Gateways

# Tasks
1. [GoTo](#1-analyze) **Analyze the current system and how a client would interact with it.**
2. [GoTo](#2-update) **Update your system architecture diagram to include the API Gateway.**
3. [GoTo](#3-implement) **Implement the API Gateway using e.g. Ocelot.**




## 1. Analyze
different from the system that are being develop by the teacher, i've gone a different direction with the development of the microservice systems. This means I already have/need a Api-gateway to handle the request from the client. In my current system i have not used Ocelot as my base gateway.

**System Description** i've those to use RabbitMQ with a Tcp-protocol, and in the current system, i'm just using text to send different messages to the services. normally i would use a more low-level protocol to stream data between the client-services for better performance and less overhead that http i know for.

so back to the original question: how a client would interact with my system. 
![](./img/client_interaction.png)
the image display how, the system i designed to handle client requests. on the image the client send some web-based request that the server handle. after handling the http request, the server create a task, that invoke all services needed to complete the task.

this way, the system is better suited for performance and at the same time isolated from the client. making the system more secure. 


## 2. Update
TODO missing this

## 3. Implement
