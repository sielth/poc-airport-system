using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using Messages;

namespace GateService.Models.GateAggregate;

public class Gate : EntityBase, IAggregateRoot
{
  [Key] public int? GateNr { get; set; }
  public string? FlightId { get; set; }
  public required DateTime From { get; set; }
  public required DateTime To { get; set; }
  public GateStatus GateStatus { get; set; }
}