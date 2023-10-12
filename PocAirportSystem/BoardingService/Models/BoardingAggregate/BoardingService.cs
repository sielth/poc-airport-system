using BoardingService.Data;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace BoardingService.Models.BoardingAggregate;

public class BoardingService : IBoardingService
{
  private readonly EfRepository<Boarding> _repository;

  public BoardingService(EfRepository<Boarding> repository)
  {
    _repository = repository;
  }

  public async Task<Boarding> AddBoardingAsync(Boarding boarding) =>
    await _repository.AddAsync(boarding);
}