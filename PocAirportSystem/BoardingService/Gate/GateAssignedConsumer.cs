using BoardingService.Checkin;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Mapster;
using MassTransit;

namespace BoardingService.Gate;

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>
{
  private readonly ICheckinCaller _checkinCaller;
  private readonly IBoardingRepository _boardingRepository;

  public GateAssignedConsumer(ICheckinCaller checkinCaller, IBoardingRepository boardingRepository)
  {
      _checkinCaller = checkinCaller;
      _boardingRepository = boardingRepository;
  }

  // task 1: consume msg from Gate service
  public async Task Consume(ConsumeContext<GateAssignedEvent> context)
    {
      // task 2: get passenger list by flight nr.
      Request request = new Request { FlightNr = context.Message.FlightNr };
      var response = await _checkinCaller.HandleAsync(request, default);
      var passengerDto = response.Passengers;

      // task 3: save to boarding db
      var passengers = passengerDto.Adapt<List<Passenger>>();
      var boarding = context.Message.Adapt<Boarding>();
      boarding.Passengers = passengers;

      await _boardingRepository.UpsertAsync(boarding);
    }
}