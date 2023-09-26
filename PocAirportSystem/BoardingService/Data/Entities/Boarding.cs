using System.ComponentModel.DataAnnotations;

namespace BoardingService.Data.Entities;

// EF Core entity
public class Boarding
{
  [Key] public string? FlightNr { get; set; }
  
  public List<Passenger> Passengers { get; set; } = new();
  
  public required int Gate { get; set; }
  public required DateTime From { get; set; }
  public required DateTime To { get; set; }
}