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
    // boarding with the old gate nr
    var boarding = await _boardingService.GetBoardingByFlightNrAsync(context.Message.FlightNr);
    ArgumentNullException.ThrowIfNull(boarding);

    boarding = context.Message.Adapt<Models.BoardingAggregate.Boarding>();
    await _boardingService.UpdateBoardingAsync(boarding);
  }
}