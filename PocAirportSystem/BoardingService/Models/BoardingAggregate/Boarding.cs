using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using BoardingService.Models.PassengerAggregate;

namespace BoardingService.Models.BoardingAggregate;

// EF Core entity
public class Boarding : EntityBase, IAggregateRoot
{
  [Key] public string? FlightNr { get; set; }
  
  public List<Passenger>? Passengers { get; set; } = new();
  
  public required int Gate { get; set; }
  public required DateTime From { get; set; }
  public required DateTime To { get; set; }
}