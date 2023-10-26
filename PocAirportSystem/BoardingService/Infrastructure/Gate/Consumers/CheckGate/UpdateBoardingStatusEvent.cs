using BoardingService.Infrastructure.Gate.Consumers.GateUpdated;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class UpdateBoardingStatusEvent
{
  public required int GateNr { get; set; }
  public required GateStatus GateStatus { get; set; }
}

public class Test : IConsumer<GateUpdatedEvent>
{
  public Task Consume(ConsumeContext<UpdateBoardingStatusEvent> context)
  {
    throw new NotImplementedException();
  }

  public Task Consume(ConsumeContext<GateUpdatedEvent> context)
  {
    throw new NotImplementedException();
  }
}