using BoardingService.Infrastructure.Gate.Consumers.GateUpdated;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class UpdateBoardingStatusEvent
{
    public required int GateNr { get; set; }
    public required string FlightNr { get; set; }
    public required GateStatus GateStatus { get; set; }
}