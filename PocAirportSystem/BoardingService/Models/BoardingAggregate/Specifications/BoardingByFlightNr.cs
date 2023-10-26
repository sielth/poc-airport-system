using Ardalis.Specification;

namespace BoardingService.Models.BoardingAggregate.Specifications;

public sealed class BoardingByFlightNr : Specification<Boarding>
{
  public BoardingByFlightNr(string flightNr)
  {
    Query
      .Where(boarding => boarding.FlightNr == flightNr);
  }
}