using Ardalis.SharedKernel;
using BoardingService.Models.PassengerAggregate.Specifications;

namespace BoardingService.Models.PassengerAggregate;

public class PassengerService : IPassengerService
{
  private readonly IRepository<Passenger> _repository;

  public PassengerService(IRepository<Passenger> repository)
  {
    _repository = repository;
  }

  public async Task<IEnumerable<Passenger>> ListPassengersByFlighNrAsync(string flightNr) =>
    await _repository.ListAsync(new PassengerByFlightNrSpec(flightNr));

  public async Task UpdatePassengerBoardingStatusAsync(string? passengerId, string? checkinNr, bool hasBoarded)
  {
    var passenger = await _repository.FirstOrDefaultAsync(
      new PassengerByPassengerIdAndCheckinNrSpec(passengerId, checkinNr));
    ArgumentNullException.ThrowIfNull(passenger);

    passenger.Status = hasBoarded;
    await _repository.UpdateAsync(passenger);
  }
}