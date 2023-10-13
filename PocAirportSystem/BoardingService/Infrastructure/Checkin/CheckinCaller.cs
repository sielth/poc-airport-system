using Flurl;
using Flurl.Http;

namespace BoardingService.Infrastructure.Checkin
{
  public class CheckinCaller : ICheckinCaller
  {
    private readonly IConfiguration _configuration;

    public CheckinCaller(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    // https://flurl.dev/docs/fluent-http/
    public async Task<Response> GetPassengersByFlightNrAsync(string flightNr,
      CancellationToken cancellationToken = new CancellationToken()) =>
      await _configuration["Urls__CheckinService"] // Base address
        .AppendPathSegment("passenger")
        .SetQueryParams(new { flightNr = flightNr })
        .GetJsonAsync<Response>(cancellationToken: cancellationToken);
  }
}