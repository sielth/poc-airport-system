using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Messages.Passenger;

namespace BoardingService.Infrastructure.Checkin.Consumers.PassengerCancelled;

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
    var passenger = await _passengerService.GetPassengerByPassengerIdAsync(context.Message.PassengerId, context.Message.CheckinNr);

    await _passengerService.DeletePassengerAsync(passenger);
  }
}