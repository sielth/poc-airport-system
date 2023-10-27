namespace BoardingService.Models.LuggageAggregate;

public interface ILuggageService
{
  Task AddLuggageAsync(Luggage boarding);
}