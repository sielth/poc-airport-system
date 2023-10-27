namespace Messages.Boarding;

public class LastCallCommand
{
  public required string FlightNr { get; set; }
  public required int GateNr { get; set; }
}