using BoardingService.Infrastructure.Gate.Consumers.GateUpdated;
using FastEndpoints;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Endpoints;

public class PublishGateUpdatedEventTestEndpoint : Endpoint<Request>
{
  public IBus? Bus { get; set; }

  public override void Configure()
  {
    Get("/api/test/UpdateGate/{GateNr}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(Request req, CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new GateUpdatedEvent
    {
      FlightNr = "0eb773dd-f2b0-4536-9f87-8a68598f9f17",
      GateNr = req.GateNr,
      From = DateTime.Now.AddMinutes(-10),
      To = DateTime.Now
    }, ct);

    await SendOkAsync(ct);
  }
}

public class Request
{
  public required int GateNr { get; set; }
}