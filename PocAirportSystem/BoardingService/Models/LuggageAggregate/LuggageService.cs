using Ardalis.SharedKernel;

namespace BoardingService.Models.LuggageAggregate;

public class LuggageService : ILuggageService
{
  private readonly IRepository<Luggage> _repository;

  public LuggageService(IRepository<Luggage> repository)
  {
    _repository = repository;
  }

  public async Task AddLuggageAsync(Luggage luggage)
  {
    await _repository.AddAsync(luggage);
    await _repository.SaveChangesAsync();
  }

  public async Task UpdateLuggageAsync(Luggage luggageToUnboard)
  {
    var luggage = await _repository.GetByIdAsync(luggageToUnboard.Id);
    ArgumentNullException.ThrowIfNull(luggage);
    
    luggage.Status = luggageToUnboard.Status;

    await _repository.UpdateAsync(luggage);
    await _repository.SaveChangesAsync();
  }
}