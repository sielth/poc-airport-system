using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using GateService.Models.GateAggregate;
using MassTransit;
using Messages;
using Messages.Boarding;
using Messages.Luggage;

namespace GateService.Infrastructure.BoardingControl.Consumers.UpdateBoardingStatus;

public class UpdateBoardingStatusConsumer : IConsumer<UpdateBoardingStatusEvent>
{
  private readonly IGateService _gateService;

  public UpdateBoardingStatusConsumer(IGateService gateService)
  {
    _gateService = gateService;
  }
  
  public async Task Consume(ConsumeContext<UpdateBoardingStatusEvent> context)
  {
    if (context.Message.GateStatus == GateStatus.Closed)
    {
      await _gateService.FreeGateAsync(context.Message.GateNr);
    }
  }
}