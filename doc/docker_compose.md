# Week 9 - Deployment using Docker
## Objectives
Containerize the microservices using Docker and outline a deployment strategy.

# Tasks
1. Dockerize Services: Write a Dockerfile for each microservice.
2. Compose Setup: Utilize Docker Compose to orchestrate service interactions.
3. Deployment Strategy: Develop a deployment plan, considering aspects like security, scalability, and disaster recovery.




# 1. Dockerize Services

**TweetIt** 
Dockerfile for the web-server
[Dockerfile](/TweetIt/dockerfile)

**Account service**
[Dockerfile](/AccountService/dockerfile)

**Post Service**
[Dockerfile](/PostService/dockerfile)

# 2. Compose Setup
````yml
services:
  account-db:
    image: mysql:latest
    container_name: account_db
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: AccountDb
      MYSQL_USER: accountuser
      MYSQL_PASSWORD: accountpassword
    ports:
      - "3306:3306"
    volumes:
      - ./sql/account_db.sql:/db/account_db.sql

  post-db:
    image: mysql:latest
    container_name: post_db
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: PostServiceDb
      MYSQL_USER: postuser
      MYSQL_PASSWORD: postpassword
    ports:
      - "3307:3306"
    volumes:
      - ./sql/post_db.sql:/db/post_db.sql

  activity-db:
    image: mysql:latest
    container_name: activity_db
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: ActivityDb
      MYSQL_USER: activityuser
      MYSQL_PASSWORD: activitypassword
    ports:
      - "3308:3306"
    volumes:
      - ./sql/activity_db.sql:/db/activity_db.sql


  tweetit_app:
    build:
      context: .
      dockerfile: TweetIt/dockerfile
    container_name: tweetIt_web
    ports:
      - "5000:80"
    depends_on:
      - rabbitmq
    environment:
      - "RABBITMQ_HOST=rabbitmq"
    networks:
      - api_network

  rabbitmq:
    image: "rabbitmq:3-management"
    container_name: "RMQ"
    ports:
      - "15672:15672"
      - "5672:5672"
    environment:
      RABBITMQ_DEFAULT_USER: "guest"
      RABBITMQ_DEFAULT_PASS: "guest"
    volumes:
      - "rabbitmq_data:/var/lib/rabbitmq"
    networks:
      - "api_network"


  post_service:
    build:
      context: .
      dockerfile: PostService/dockerfile
    depends_on:
      - rabbitmq
    environment:
      - "RABBITMQ_HOST=rabbitmq"

    deploy:
      replicas: 3
      restart_policy:
        condition: on-failure

    networks:
      - "api_network"

  account_service:
    build:
      context: .
      dockerfile: AccountService/dockerfile
    depends_on:
      - rabbitmq
    environment:
      - "RABBITMQ_HOST=rabbitmq"
    networks:
      - "api_network"
    deploy:
      replicas: 3
      restart_policy:
        condition: on-failure

volumes:
  rabbitmq_data:

networks:
  api_network:
    driver: bridge
````
[Docker-compose](/docker-compose.yml)


While I attempted to create a Docker Compose file, I also focused on implementing network security and fallback handling for the services.


# 3. Deployment Strategy
For the most part, I've tried to set up how each container should handle failures during operations. Both network and restart policies have been created.

All services are designed to wait for the RabbitMQ broker server, both inside the Docker Compose file as well as in the code itself, providing a safeguard against any potential issues or downtime.