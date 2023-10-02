using BoardingService.Data;
using BoardingService.Models.BoardingAggregate;

namespace BoardingService.Models.PassengerAggregate;

public interface IPassengerRepository : IRepository<Boarding>
{
  Task<Boarding> GetByIdAsync(string passengerId, string checkinNr);
}