using BoardingService.Models.BoardingAggregate;

namespace BoardingService.Models.PassengerAggregate;

public class PassengerRepository : IPassengerRepository
{
  public Task<IEnumerable<Boarding>> ListAsync()
  {
    throw new NotImplementedException();
  }

  public Task UpsertAsync(Boarding entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Boarding entity)
  {
    throw new NotImplementedException();
  }

  public Task<Boarding> GetByIdAsync(string passengerId, string checkinNr)
  {
    throw new NotImplementedException();
  }
}