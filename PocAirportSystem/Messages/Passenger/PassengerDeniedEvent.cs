namespace Messages.Passenger;

public class PassengerDeniedEvent
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
}