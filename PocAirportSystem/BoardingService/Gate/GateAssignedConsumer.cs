using MassTransit;

namespace BoardingService.Gate;

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>
{
  public Task Consume(ConsumeContext<GateAssignedEvent> context)
  {
    // TODO: Save GateNr, FlightNr, FromTime, ToTime to database
    
    throw new NotImplementedException();
  }
}