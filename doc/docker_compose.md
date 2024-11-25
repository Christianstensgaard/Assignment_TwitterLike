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
![Docker-compose](/docker-compose.yml)

# 3. Deployment Strategy
for the most part i've tried to setup how the different containers should handle fail doing operation. here both network and restart policy are created.  