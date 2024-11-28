# Week 41 - Testing
## Objectives
Develop and execute a testing strategy.

# Tasks
1. Unit Testing: Create unit tests for each service's core functionalities.
2. Component Testing: Test microservices in isolation to ensure they function as expected.
3. (Optional) Contract Testing: Perform integration tests to ensure seamless service interaction.

# 1. Unit Testing
been had a lot of problems with moq and to get it to work, so if running this, some might throw a error.
but tried to create some, and they are displayed under.

[Account Test](/TweetItTest/AccountTest.cs)

[Rabbit MQ](/TweetItTest/RabbitMQ_test.cs)

[Account RMQ Test](/TweetItTest/Account_direct_test.cs)

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

After experiencing difficulties with MOQ, I decided to explore the use of Docker containers for unit testing. However, my progress on this endeavor has been hindered by various technical challenges and other commitments, preventing me from fully completing the project at this time."


# 3. Contract Testing
***key-values***

1. Increased confidence: By verifying that the system meets the specified contracts, you can increase your confidence in its behavior.


2. Improved maintainability: Contract testing helps ensure that changes to the system do not break existing functionality or introduce new bugs.


3. Reduced complexity: By focusing on external interactions, contract testing helps simplify the testing process by reducing the number of test cases needed.