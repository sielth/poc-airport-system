namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class CheckGateCommand
{
  public required string FlightNr { get; set; }
  public required int GateNr { get; set; }
  public required DateTime From { get; set; }
  public required DateTime To { get; set; }
}