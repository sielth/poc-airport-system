namespace Messages.Luggage;

public class LuggageRemovedEvent
{
  public required string? PassengerId { get; set; }
  public required string? LuggageId { get; set; }
}