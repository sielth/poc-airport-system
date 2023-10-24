namespace BoardingService.Infrastructure.Checkin.Callers
{
  public class PassengerDto
  {
    public required string PassengerId { get; set; }
    public required string CheckinNr { get; set; }
  }
}