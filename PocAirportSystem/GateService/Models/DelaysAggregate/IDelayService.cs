namespace GateService.Models.DelaysAggregate;

public interface IDelayService
{
  Task AddDelayAsync(Delay delay);
  Task<Delay?> GetDelayByFlightNrAsync(string flightNr);
  Task DeleteDelayByFlightNrAsync(string flightNr);
}