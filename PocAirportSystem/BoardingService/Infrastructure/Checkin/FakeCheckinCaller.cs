using BoardingService.Models.PassengerAggregate;
using Mapster;

namespace BoardingService.Infrastructure.Checkin;

public class FakeCheckinCaller : ICheckinCaller
{
  private readonly IPassengerService _passengerService;

  public FakeCheckinCaller(IPassengerService passengerService)
  {
    _passengerService = passengerService;
  }

  public async Task<Response> GetPassengersByFlightNrAsync(string flightNr,
    CancellationToken cancellationToken = new CancellationToken())
  {
    var passengers = await _passengerService.ListPassengersByFlighNrAsync(flightNr);
    foreach (var passenger in passengers)
    {
      await _passengerService.UpdatePassengerBoardingStatusAsync(passenger,
        hasBoarded: false);
    }

    var passengersDto = passengers.Adapt<IEnumerable<PassengerDto>>();

    return new Response { Passengers = passengersDto };
  }
}