namespace Messages.Gate;

public class GateUpdatedEvent
{
  public required string FlightNr { get; set; }
  public required int GateNr { get; set; }
  public required DateTime From { get; set; }
  public required DateTime To { get; set; }
}