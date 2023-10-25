using BoardingService.Models.BoardingAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers;

public class GateUpdatedConsumer : IConsumer<GateUpdatedEvent>
{
  private readonly IBus _bus;
  private readonly IBoardingService _boardingService;

  public GateUpdatedConsumer(IBus bus, IBoardingService boardingService)
  {
    _bus = bus;
    _boardingService = boardingService;
  }

  public async Task Consume(ConsumeContext<GateUpdatedEvent> context)
  {
    // TODO: get boarding by flightnr, update boarding
  }
}