using Ardalis.Specification;

namespace BoardingService.Models.BoardingAggregate.Specifications;

public sealed class BoardingByGateAndDateTimeWithPassengers : Specification<Boarding>
{
  public BoardingByGateAndDateTimeWithPassengers(int gate, DateTime scanTime)
  {
    Query
      .Where(boarding => boarding.GateNr == gate &&
                         (scanTime > boarding.From && scanTime < boarding.To))
      .Include(boarding => boarding.Passengers);
  }
}