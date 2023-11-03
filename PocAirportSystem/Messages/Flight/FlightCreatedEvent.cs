namespace Messages.Flight;

public class FlightCreatedEvent
{
  public int? Id { get; set; }
  public string? ToLocation { get; set; }
  public string? FromLocation { get; set; }
  public required DateTime ArrivalDate { get; set; }
  public required DateTime DepartureDate { get; set; }
  public required int FlightId { get; set; }
  public required string Status { get; set; }
}