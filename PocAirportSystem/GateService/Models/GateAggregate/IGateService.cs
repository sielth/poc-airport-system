namespace GateService.Models.GateAggregate;

public interface IGateService
{
  Task AddGateAsync(Gate gate);
  Task<IEnumerable<Gate>> ListGatesAsync();
  Task UpdateGateAsync(Gate gate);
  Task<Gate?> GetAvailableGateAsync();
  Task<Gate?> GetGateByFlightNrAsync(string flightNr);
  Task FreeGateAsync(int gateNr);
}