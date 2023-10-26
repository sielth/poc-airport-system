namespace BoardingService.BoardingService;

public class PassengerBoardedEvent
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
}