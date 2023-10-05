using BoardingService.Data;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace BoardingService.Models.BoardingAggregate;

public class BoardingRepository : IBoardingRepository
{
  private readonly AppDbContext _dbContext;

  public BoardingRepository(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<IEnumerable<Boarding>> ListAsync() => await _dbContext.Boardings
    .Include(boarding => boarding.Passengers)
    .ToListAsync();

  public Task UpsertAsync(Boarding entity)
  {
    throw new NotImplementedException();
  }

  public Task DeleteAsync(Boarding entity)
  {
    throw new NotImplementedException();
  }

  public async Task<Boarding?> GetByIdAsync(string flightNr) => await _dbContext.Boardings
    .Include(boarding => boarding.Passengers)
    .FirstOrDefaultAsync(boarding => boarding.FlightNr == flightNr);
}