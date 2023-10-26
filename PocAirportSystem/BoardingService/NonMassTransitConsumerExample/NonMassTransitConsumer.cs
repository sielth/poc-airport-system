using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BoardingService.NonMassTransitConsumerExample;

public static class NonMassTransitConsumer
{
  public static void Consume()
  {
    var factory = new ConnectionFactory { HostName = "localhost", VirtualHost = "ucl"};
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.ExchangeDeclare(exchange: "BoardingService.BoardingService:PassengerBoardedEvent",
      type: ExchangeType.Fanout, durable: true);
    
    var queueName = channel.QueueDeclare(
      durable: false,
      exclusive: false,
      autoDelete: false,
      arguments: null).QueueName;
    
    channel.QueueBind(queue: queueName,
      exchange: "BoardingService.BoardingService:PassengerBoardedEvent",
      routingKey: "");

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      Console.WriteLine($" [x] Received {message}");
    };
    
    channel.BasicConsume(queue: queueName,
      autoAck: true,
      consumer: consumer);

  }
}