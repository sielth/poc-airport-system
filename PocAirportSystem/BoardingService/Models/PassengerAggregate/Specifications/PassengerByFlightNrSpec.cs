using Ardalis.Specification;

namespace BoardingService.Models.PassengerAggregate.Specifications;

public class PassengerByFlightNrSpec : Specification<Passenger>
{
  public PassengerByFlightNrSpec(string flightNr)
  {
    Query
      .Where(passenger => passenger.FlightNr == flightNr);
  }
}