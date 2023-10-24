namespace BoardingService.Infrastructure.Checkin.Consumers;

public class PassengerCancelledEvent
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
}