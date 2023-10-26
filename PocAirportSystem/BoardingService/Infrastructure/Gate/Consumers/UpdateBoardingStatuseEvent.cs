namespace BoardingService.Infrastructure.Gate.Consumers;

public class UpdateBoardingStatuseEvent
{
  public required int GateNr { get; set; }
  public required GateStatus GateStatus { get; set; }
}