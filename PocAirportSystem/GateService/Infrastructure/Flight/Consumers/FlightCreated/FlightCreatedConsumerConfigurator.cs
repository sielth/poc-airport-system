using MassTransit;
using RabbitMQ.Client;

namespace GateService.Infrastructure.Flight.Consumers.FlightCreated;

public static class FlightCreatedConsumerConfigurator
{
  public static void ConfigureFlightCreated(this IRabbitMqBusFactoryConfigurator factoryConfigurator, IBusRegistrationContext context)
  {
    factoryConfigurator.ReceiveEndpoint("FlightCreated", x =>
    {
      x.UseRawJsonDeserializer(isDefault: true);
      x.ConfigureConsumeTopology = false;
      x.ConfigureConsumer<FlightCreatedConsumer>(context);
      x.Bind("FlightJourney", s =>
      {
        s.RoutingKey = "Journey.Created.Boarding";
        s.ExchangeType = ExchangeType.Direct;
      });
    });
  }
}