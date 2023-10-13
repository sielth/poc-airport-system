namespace BoardingService.Infrastructure.Checkin
{
  public interface ICheckinCaller
  {
    Task<Response> GetPassengersByFlightNrAsync(string flightNr,
      CancellationToken cancellationToken = new CancellationToken());
  }
}