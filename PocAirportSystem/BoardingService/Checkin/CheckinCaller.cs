using Flurl;
using Flurl.Http;

namespace BoardingService.Checkin;

public class CheckinCaller : ICheckinCaller 
{
  // This class calls an endpoint with a flightNr in the query
  // Returns a list of checked in Passengers 

  // https://flurl.dev/docs/fluent-http/
  public async Task<Response> HandleAsync(Request request,
    CancellationToken cancellationToken = new CancellationToken())
  {
    var person = await "https://api.com" // Base address
      .AppendPathSegment("request")
      .SetQueryParams(new { a = 1, b = 2 })
      .WithOAuthBearerToken("my_oauth_token")
      .GetJsonAsync<Response>();

    // TODO: Save passengers to database relating them to the flightNr
    throw new NotImplementedException();
  }
}

public interface ICheckinCaller
{
  Task<Response> HandleAsync(Request request,
    CancellationToken cancellationToken = new CancellationToken());
}