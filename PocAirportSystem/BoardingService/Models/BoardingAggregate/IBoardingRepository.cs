using BoardingService.Data;

namespace BoardingService.Models.BoardingAggregate;

public interface IBoardingRepository : IRepository<Boarding>
{
  Task<Boarding> GetByIdAsync(string flightNr);
}