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

  public async Task UpdatePassengerBoardingStatusAsync(Passenger passengerToUpdate, bool hasBoarded)
  {
    var passenger = await _repository.FirstOrDefaultAsync(
      new PassengerByPassengerIdAndCheckinNrSpec(passengerToUpdate.PassengerId, passengerToUpdate.CheckinNr));
    ArgumentNullException.ThrowIfNull(passenger);

    passenger.Status = hasBoarded;
    await _repository.UpdateAsync(passenger);
  }

  public async Task<Passenger> GetPassengerByPassengerIdAsync(string passengerId, string checkinNr)
  {
    var passenger = await _repository.FirstOrDefaultAsync(
        new PassengerByPassengerIdSpec(passengerId, checkinNr));
    
    ArgumentNullException.ThrowIfNull(passenger);
    return passenger;
  }

  public async Task DeletePassengerAsync(Passenger passenger) => await _repository.DeleteAsync(passenger);

    public async Task UpdatePassengerLuggageAsync(Passenger passenger)
    {   var passengerMatch = await GetPassengerByPassengerIdAsync(passenger.PassengerId,passenger.CheckinNr);
        passengerMatch = passenger;
       await _repository.UpdateAsync(passengerMatch);
       await _repository.SaveChangesAsync();
    }
}