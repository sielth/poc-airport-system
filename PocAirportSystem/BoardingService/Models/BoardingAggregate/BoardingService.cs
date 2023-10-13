using Ardalis.SharedKernel;

namespace BoardingService.Models.BoardingAggregate;

public class BoardingService : IBoardingService
{
  private readonly IRepository<Boarding> _repository;

  public BoardingService(IRepository<Boarding> repository)
  {
    _repository = repository;
  }

  public async Task<Boarding> AddBoardingAsync(Boarding boarding) =>
    await _repository.AddAsync(boarding);

  public async Task<IEnumerable<Boarding>> ListBoardingsAsync() => await _repository.ListAsync();
}