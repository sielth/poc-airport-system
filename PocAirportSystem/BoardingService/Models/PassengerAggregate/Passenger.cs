using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.SharedKernel;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;

namespace BoardingService.Models.PassengerAggregate;

// EF Core entity 
public class Passenger : EntityBase, IAggregateRoot
{
  public string? PassengerId { get; set; }
  public string? CheckinNr { get; set; }
  
  [ForeignKey("FlightNr")] public required string FlightNr { get; set; }
  public Boarding? Boarding { get; set; }
  
  public bool Status { get; set; } // IsBoarded

    public List<Luggage>? Luggages { get; set; }= new List<Luggage>();
}