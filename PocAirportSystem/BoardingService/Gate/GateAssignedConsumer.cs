using BoardingService.Checkin;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Mapster;
using MassTransit;

namespace BoardingService.Gate;

public class Besked2
{
}

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>, IConsumer<Besked2>
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
    var request = new Request { FlightNr = context.Message.FlightNr };
    var response = await _checkinCaller.HandleAsync(request, default);
    var passengerDto = response.Passengers;

    // task 3: save to boarding db
    var passengers = passengerDto.Adapt<List<Passenger>>();
    var boarding = context.Message.Adapt<Boarding>();
    boarding.Passengers = passengers;

    await _boardingService.AddBoardingAsync(boarding);
  }

  public Task Consume(ConsumeContext<Besked2> context)
  {
    Console.WriteLine("Besked2");
    return Task.CompletedTask;
  }
}