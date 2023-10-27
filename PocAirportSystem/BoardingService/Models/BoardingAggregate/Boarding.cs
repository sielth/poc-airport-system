using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using BoardingService.Models.PassengerAggregate;

namespace BoardingService.Models.BoardingAggregate;

// EF Core entity
public class Boarding : EntityBase, IAggregateRoot
{
  [Key] public string? FlightNr { get; set; }
  
  public List<Passenger>? Passengers { get; set; } = new();
  
  public int? GateNr { get; set; } 
  public DateTime? From { get; set; }
  public DateTime? To { get; set; }
  public bool? Status { get; set; }
}