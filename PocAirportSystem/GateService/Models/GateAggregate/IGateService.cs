namespace GateService.Models.GateAggregate;

public interface IGateService
{
  Task<Gate> AddGateAsync(Gate gate);
  Task<IEnumerable<Gate>> ListGatesAsync();
  Task UpdateGateAsync(Gate gate);
  Task<Gate?> GetAvailableGateAsync();
}