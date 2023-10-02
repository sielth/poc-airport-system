namespace BoardingService.Data;

public interface IRepository<T>
{
  Task<IEnumerable<T>> ListAsync();
  Task UpsertAsync(T entity);
  Task DeleteAsync(T entity);
}