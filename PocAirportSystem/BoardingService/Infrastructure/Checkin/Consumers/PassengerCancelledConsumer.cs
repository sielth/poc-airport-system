using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.Checkin.Consumers;

public class PassengerCancelledConsumer : IConsumer<PassengerCancelledEvent>
{
  private readonly IPassengerService _passengerService;
  private readonly ILogger<PassengerCancelledConsumer> _logger;

  public PassengerCancelledConsumer(IPassengerService passengerService, ILogger<PassengerCancelledConsumer> logger)
  {
    _passengerService = passengerService;
    _logger = logger;
  }
  public async Task Consume(ConsumeContext<PassengerCancelledEvent> context)
  {
    Passenger passenger = await _passengerService.GetPassengerByPassengerIdAsync(context.Message.PassengerId);

    _passengerService.DeletePassengerAsync(passenger);
  }
}