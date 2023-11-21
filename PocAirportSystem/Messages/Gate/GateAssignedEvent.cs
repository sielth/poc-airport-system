﻿namespace Messages.Gate;

public class GateAssignedEvent
{
  public required string FlightNr { get; set; }
  public required int GateNr { get; set; }
  public required DateTime GateStartTime { get; set; }
  public required DateTime GateEndTime { get; set; }
}
