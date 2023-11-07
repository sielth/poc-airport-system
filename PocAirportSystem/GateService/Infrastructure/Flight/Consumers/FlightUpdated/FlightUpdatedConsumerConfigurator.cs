using MassTransit;
using RabbitMQ.Client;

namespace GateService.Infrastructure.Flight.Consumers.FlightUpdated;

public static class FlightUpdatedConsumerConfigurator
{
  public static void ConfigureFlightUpdated(this IRabbitMqBusFactoryConfigurator factoryConfigurator, IBusRegistrationContext context)
  {
    factoryConfigurator.ReceiveEndpoint("FlightUpdated", x =>
    {
      x.UseRawJsonDeserializer(isDefault: true);
      x.ConfigureConsumeTopology = false;
      x.ConfigureConsumer<FlightUpdatedConsumer>(context);
      x.Bind("FlightJourney", s =>
      {
        s.RoutingKey = "Journey.Updated.Boarding";
        s.ExchangeType = ExchangeType.Direct;
      });
    });
  }
}