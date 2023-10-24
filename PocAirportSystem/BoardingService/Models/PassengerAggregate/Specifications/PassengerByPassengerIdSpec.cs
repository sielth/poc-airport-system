using Ardalis.Specification;

namespace BoardingService.Models.PassengerAggregate.Specifications;

public sealed class PassengerByPassengerIdSpec : Specification<Passenger>
{
  public PassengerByPassengerIdSpec(string passengerId)
  {
    Query
      .Where(passenger => passenger.PassengerId == passengerId);
  }
}