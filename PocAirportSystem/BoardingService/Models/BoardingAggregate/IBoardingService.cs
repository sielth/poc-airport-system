using BoardingService.Data;

namespace BoardingService.Models.BoardingAggregate;

public interface IBoardingService
{
  Task<Boarding> AddBoardingAsync(Boarding boarding);
}