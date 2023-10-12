using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.SharedKernel;
using BoardingService.Models.BoardingAggregate;

namespace BoardingService.Models.PassengerAggregate;

// EF Core entity 
public class Passenger : EntityBase, IAggregateRoot
{
  // TODO: Research whether it is normalized and eventually how to tell EF Core they are both primary keys
  [Key] public string? PassengerId { get; set; }
  [Key] public string? CheckinNr { get; set; }
  
  [ForeignKey("FlightNr")] public required string FlightNr { get; set; }
  public Boarding? Boarding { get; set; }
  
  public bool Status { get; set; } // IsBoarded
}