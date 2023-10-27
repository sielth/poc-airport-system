using FastEndpoints;
using Messages.Passenger;

namespace BoardingService.Infrastructure.Checkin.Endpoints;

public class PassengerCancelledTestEndpoint : Endpoint<PassengerRequest>
{
  public override void Configure()
  {
    Get("/api/test/passenger/{boarded}");
    AllowAnonymous();
  }
}
public class PassengerRequest
{
  public required int PassengerId { get; set; }
}