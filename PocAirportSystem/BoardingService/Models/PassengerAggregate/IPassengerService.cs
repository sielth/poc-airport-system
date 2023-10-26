namespace BoardingService.Models.PassengerAggregate;

public interface IPassengerService
{
  Task<IEnumerable<Passenger>> ListPassengersByFlighNrAsync(string flightNr);
  Task UpdatePassengerBoardingStatusAsync(Passenger passenger,  bool hasBoarded);
  Task<Passenger> GetPassengerByPassengerIdAsync(string messagePassengerId, string checkinNr);
  Task DeletePassengerAsync(Passenger passenger);
}