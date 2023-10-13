namespace BoardingService.Models.PassengerAggregate;

public interface IPassengerService
{
  Task<IEnumerable<Passenger>> ListPassengersByFlighNrAsync(string flightNr);
  Task UpdatePassengerBoardingStatusAsync(string? passengerId, string? checkinNr, bool hasBoarded);
}