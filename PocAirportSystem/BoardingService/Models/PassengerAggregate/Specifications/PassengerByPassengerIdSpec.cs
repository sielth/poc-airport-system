using Ardalis.Specification;

namespace BoardingService.Models.PassengerAggregate.Specifications;

public sealed class PassengerByPassengerIdSpec : Specification<Passenger>
{
  public PassengerByPassengerIdSpec(string passengerId, string checkinNr)
  {
    Query
      .Where(passenger => passenger.PassengerId == passengerId && passenger.CheckinNr == checkinNr);
  }
}