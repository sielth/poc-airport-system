using Ardalis.ApiEndpoints;

namespace BoardingService.Checkin;

public class CheckinCaller : EndpointBaseAsync.WithRequest<Request>.WithResult<Response>
{
  // This class calls an endpoint with a flightNr in the query
  // Returns a list of checked in Passengers 
  
  public override Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
  {
    // TODO: Save passengers to database relating them to the flightNr
    throw new NotImplementedException();
  }
}