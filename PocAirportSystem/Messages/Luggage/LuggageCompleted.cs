namespace Messages.Luggage;

public class LuggageCompleted
{
  public required string PassengerId { get; set; }
  public required int GateNumber { get; set; }
  public required string FlightNumber { get; set; }
  public required string CheckedInNumber { get; set; }
  public required int BaggageId { get; set; }
}