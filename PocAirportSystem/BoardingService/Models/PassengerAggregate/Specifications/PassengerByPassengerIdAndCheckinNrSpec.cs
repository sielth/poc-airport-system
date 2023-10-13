using Ardalis.Specification;

namespace BoardingService.Models.PassengerAggregate.Specifications;

public class PassengerByPassengerIdAndCheckinNrSpec : Specification<Passenger>
{
  public PassengerByPassengerIdAndCheckinNrSpec(string? passengerId, string? checkinNr)
  {
    Query
      .Where(passenger => passenger.PassengerId == passengerId && passenger.CheckinNr == checkinNr);
  }
}