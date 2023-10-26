using BoardingService.Models.BoardingAggregate;
using Mapster;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers.GateUpdated;

public class GateUpdatedConsumer : IConsumer<GateUpdatedEvent>
{
  private readonly IBoardingService _boardingService;

  public GateUpdatedConsumer(IBoardingService boardingService)
  {
    _boardingService = boardingService;
  }

  public async Task Consume(ConsumeContext<GateUpdatedEvent> context)
  {
    var boarding = context.Message.Adapt<Boarding>();
    boarding.Gate = context.Message.GateNr; // temp fix
    await _boardingService.UpdateBoardingAsync(boarding);
  }
}