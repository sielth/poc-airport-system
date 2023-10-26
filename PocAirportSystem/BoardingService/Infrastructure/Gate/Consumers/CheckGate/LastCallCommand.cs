namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class LastCallCommand
{
  public required string FlightNr { get; set; }
  public required int GateNr { get; set; }
}