namespace GateService.Models.DelaysAggregate;

public interface IDelayService
{
  Task<Delay?> GetDelayByFlightNrAsync(string flightNr);
  Task DeleteDelayByFlightNrAsync(string flightNr);
}