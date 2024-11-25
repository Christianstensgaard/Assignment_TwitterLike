# Week 41 - Testing
## Objectives
Develop and execute a testing strategy.

# Tasks
1. Unit Testing: Create unit tests for each service's core functionalities.
2. Component Testing: Test microservices in isolation to ensure they function as expected.
3. (Optional) Contract Testing: Perform integration tests to ensure seamless service interaction.
Expectations

# 1. Unit Testing
been had a lot of problems with moq and to get it to work, so if running this, some might throw a error.
but tried to create some, and they are displayed under.
![Account Test](/TweetItTest/AccountTest.cs)
![Rabbit MQ](/TweetItTest/RabbitMQ_test.cs)
![Account RMQ Test](/TweetItTest/Account_direct_test.cs)

# 2. Component Testing
```
version: '3.9'
services:
  rabbitmq:
    image: rabbitmq:management
    ports:
      - "5672:5672"
      - "15672:15672"
  database:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: test_db
    ports:
      - "3306:3306"
```

had problems with MOQ and therefore, i've been thinking doing unit tests inside a docker environment. i have not had the time to finish this completely due to all the problems i encountered.  


# 3. Contract Testing
did not have the time to create this, but done some research on how it could be implemented, so here are some ideas how this could be done.

TODO missing more here