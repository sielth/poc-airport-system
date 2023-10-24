using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.Checkin.Consumers;

public class PassengerCheckedinConsumer : IConsumer<PassengerCheckedinEvent>
{
  private readonly IBoardingService _boardingService;
  private readonly ILogger<PassengerCheckedinConsumer> _logger;

  public PassengerCheckedinConsumer(IBoardingService boardingService, ILogger<PassengerCheckedinConsumer> logger)
  {
    _boardingService = boardingService;
    _logger = logger;
  }

  public async Task Consume(ConsumeContext<PassengerCheckedinEvent> context)
  {
    Boarding boarding = await _boardingService.GetBoardingByFlightNrAsync(context.Message.FlightNr);
    if (boarding == null)
    {
      new Boarding { FlightNr = context.Message.FlightNr };
    }

    boarding!.Passengers?.Add(new Passenger 
    { 
      PassengerId = context.Message.PassengerId,
      CheckinNr = context.Message.CheckinNr,
      FlightNr = context.Message.FlightNr
    });

    await _boardingService.AddBoardingAsync(boarding);
  }
}