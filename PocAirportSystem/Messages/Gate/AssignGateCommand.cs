using MassTransit;

namespace Messages.Gate;

public class AssignGateCommand
{
  public required string FlightNr { get; set; }
  public required DateTime GateStartTime { get; set; }
  public required DateTime GateEndTime { get; set; }
}