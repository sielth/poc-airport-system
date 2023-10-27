using FastEndpoints;
using MassTransit;
using Messages.Passenger;

namespace BoardingService.Infrastructure.Checkin.Endpoints;

public class PassengerCancelledTestEndpoint : Endpoint<CancelPassengerRequest>
{
  public IBus? Bus { get; set; }
  
  public override void Configure()
  {
    Get("/api/test/CancelPassenger");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancelPassengerRequest req, CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new PassengerCancelledEvent{ // example of the kind of data we get from the check in group
      PassengerId = req.PassengerId,
      CheckinNr = req.CheckinNr
    }, ct);

    await SendOkAsync(ct);
  }
}
public class CancelPassengerRequest
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
}