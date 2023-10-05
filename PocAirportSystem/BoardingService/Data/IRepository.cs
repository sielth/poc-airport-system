using BoardingService.Models.BoardingAggregate;

namespace BoardingService.Data;

public interface IRepository<T>
{
  Task<IEnumerable<Boarding>> ListAsync();
  Task UpsertAsync(T entity);
  Task DeleteAsync(T entity);
}