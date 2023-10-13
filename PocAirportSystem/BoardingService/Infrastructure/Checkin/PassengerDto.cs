namespace BoardingService.Infrastructure.Checkin
{
  public class PassengerDto
  {
    public required string PassengerId { get; set; }
    public required string CheckinNr { get; set; }
  }
}