namespace GateService.Models.DelaysAggregate;

public class DelayService : IDelayService
{
  public Task AddDelayAsync(Delay delay)
  {
    throw new NotImplementedException();
  }

  public Task<Delay?> GetDelayByFlightNrAsync(string flightNr)
  {
    throw new NotImplementedException();
  }

  public Task DeleteDelayByFlightNrAsync(string flightNr)
  {
    throw new NotImplementedException();
  }
}