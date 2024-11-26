# Tasks
1. Identify key areas of failure in your system.
2. Propose and implement mitigations for the identified failure points.
3. Document how you've made features of your system more reliable.

# Expectations
A thoughtful analysis of your system and how to improve its reliability and a set of mitigations that address the identified failure points.



## 1. Identify key areas...
first is there is nothing to handle different error handling inside the system, this means that if something goes wrong, nothing happens and the client would be left wondering what is going on.

therefore there is a reliability problem on each service connected to the RabbitMQ server, meaning there is no clear understanding between the services what should happen if something went wrong.


## 2. propose and implement mitigation...
i've been looking a bit for what patten and design that could help the system to handle different types of errors, and after some reading and playing around using **circuit-breaker** design patten could solve a lot of problems adding a feature the handle errors inside the system. 

**my solution would look something like this.**
![](./img/fallback.png)



## 3. Document how you've made features

[RabbitMqDefault/Tools/CircuitBreaker.cs](/RabbitMqDefault/Tools/CircuitBreaker.cs)

implementing a simple circuit breaker, can create a clear way to handle different kind of errors inside the services. while creating the patten i added some common fallback mechanism 

1. maxFailures
2. timeout

to use this class i've modified **RabbitMQ** library so with try catch, the patten can easy be used by the different services. 

*here i used the Circuit breaker, to create a request for a logout*
[codeSnippets/Circuitbreaker.cs](./codeSnippets/Circuitbreaker.cs)

moving to the logout service inside the Account-service we can see how this is handled
[codeSnippets/callbackCircuitbreaker.cs](./codeSnippets/callbackCircuitbreaker.cs)

by doing this we have create a fallback policy, by using the circuit-breaker design patten. 