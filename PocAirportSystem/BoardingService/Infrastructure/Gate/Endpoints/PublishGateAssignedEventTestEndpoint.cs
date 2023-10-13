using BoardingService.Infrastructure.Gate.Consumers;
using FastEndpoints;
using MassTransit;
using MassTransit.Mediator;

namespace BoardingService.Infrastructure.Gate.Endpoints;

public class PublishGateAssignedEventTestEndpoint : Endpoint<Request>
{
  public IBus? Bus { get; set; }

  public override void Configure()
  {
    Get("/api/test/{flightNr}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request req, CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new GateAssignedEvent
    {
      FlightNr = "0eb773dd-f2b0-4536-9f87-8a68598f9f17",
      GateNr = 86,
      From = DateTime.Now.AddMinutes(-10),
      To = DateTime.Now
    }, ct);

    await SendOkAsync(ct);
  }
}

public class Request
{
  public required string FlightNr { get; set; }
}