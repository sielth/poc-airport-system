using Ardalis.SharedKernel;

namespace GateService.Models.GateAggregate;

public class GateService : IGateService
{
  private readonly IRepository<Gate> _repository;

  public GateService(IRepository<Gate> repository)
  {
    _repository = repository;
  }

  public Task<Gate> AddGateAsync(Gate gate)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Gate>> ListGatesAsync()
  {
    throw new NotImplementedException();
  }

  public Task UpdateGateAsync(Gate gate)
  {
    throw new NotImplementedException();
  }

  public Task<Gate> GetAvailableGateAsync()
  {
    throw new NotImplementedException();
  }
}