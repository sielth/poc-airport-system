namespace BoardingService.Models.BoardingAggregate;

public interface IBoardingService
{
  Task<Boarding> AddBoardingAsync(Boarding boarding);
  Task<IEnumerable<Boarding>> ListBoardingsAsync();
  Task<Boarding> GetBoardingByGateAndDateTimeWithPassengersAsync(int gate, DateTime scanTime);
  Task<Boarding?> GetBoardingByFlightNrAsync(string flightNr);
  Task UpdateBoardingAsync(Boarding boarding);
}