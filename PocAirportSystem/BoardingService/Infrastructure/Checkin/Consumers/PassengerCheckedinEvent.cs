namespace BoardingService.Infrastructure.Checkin.Consumers;

public class PassengerCheckedinEvent
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
  public required string FlightNr { get; set; }
}