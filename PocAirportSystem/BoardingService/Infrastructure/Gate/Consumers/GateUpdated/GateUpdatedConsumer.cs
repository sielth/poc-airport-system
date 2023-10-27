using BoardingService.Models.BoardingAggregate;
using MassTransit;
using Messages.Gate;

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
    var boarding = new Boarding
    {
      FlightNr = context.Message.FlightNr,
      Gate = context.Message.GateNr,
      From = context.Message.From.ToUniversalTime(),
      To = context.Message.To.ToUniversalTime()
    };
    await _boardingService.UpdateBoardingAsync(boarding);
  }
}