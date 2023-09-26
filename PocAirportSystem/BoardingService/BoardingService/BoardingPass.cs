namespace BoardingService.BoardingService;

public class BoardingPass
{
  public required string PassId { get; set; }
  public required string CheckinNr { get; set; }
  public required string GateNr { get; set; }
  public required DateTime Now { get; set; }
}