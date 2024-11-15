# Use the official RabbitMQ image from Docker Hub
FROM rabbitmq:3-management

# Expose the ports for RabbitMQ
# 5672 is the default port for RabbitMQ messaging
# 15672 is the default port for RabbitMQ management UI
EXPOSE 5672
EXPOSE 15672

# The RabbitMQ image will run RabbitMQ automatically
# No additional command needed
