using BoardingService.Infrastructure.Checkin;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Mapster;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers;

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>
{
  private readonly ICheckinCaller _checkinCaller;
  private readonly IBoardingService _boardingService;

  public GateAssignedConsumer(ICheckinCaller checkinCaller, IBoardingService boardingService)
  {
    _checkinCaller = checkinCaller;
    _boardingService = boardingService;
  }

  // task 1: consume msg from Gate service
  public async Task Consume(ConsumeContext<GateAssignedEvent> context)
  {
    // task 2: get passenger list by flight nr.
    var response = await _checkinCaller.GetPassengersByFlightNrAsync(context.Message.FlightNr);
    var passengerDto = response.Passengers;

    // task 3: save to boarding db
    var passengers = passengerDto.Adapt<List<Passenger>>();
    var boarding = context.Message.Adapt<Boarding>();
    boarding.Passengers = passengers;

    await _boardingService.AddBoardingAsync(boarding);
  }
}