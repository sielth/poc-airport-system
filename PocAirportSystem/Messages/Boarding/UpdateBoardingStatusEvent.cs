namespace Messages.Boarding;

public class UpdateBoardingStatusEvent
{
    public required int GateNr { get; set; }
    public required string FlightNr { get; set; }
    public required GateStatus GateStatus { get; set; }
}