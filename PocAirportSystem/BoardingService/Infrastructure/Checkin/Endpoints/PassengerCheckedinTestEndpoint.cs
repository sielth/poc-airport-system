using FastEndpoints;
using MassTransit;
using Messages.Passenger;

namespace BoardingService.Infrastructure.Checkin.Endpoints;

public class PassengerCheckedInTestEndpoint : Endpoint<CheckinPassengerRequest>
{
  public IBus? Bus { get; set; }
  
  public override void Configure()
  {
    Post("/api/test/CheckinPassenger");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CheckinPassengerRequest req, CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new PassengerCheckedinEvent{ // example of the kind of data we get from the check in group
      PassengerId = req.PassengerId,
      CheckinNr = req.CheckinNr,
      FlightNr = req.FlightNr
    }, ct);

    await SendOkAsync(ct);
  }
}
public class CheckinPassengerRequest
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
  public required string FlightNr { get; set; }
}