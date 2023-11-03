using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;

namespace GateService.Models.DelaysAggregate;

public class Delay :  EntityBase, IAggregateRoot
{
  [Key] public string? FlightId { get; set; }
  public required DateTime NewFrom { get; set; }
  public required DateTime NewTo { get; set; }
}