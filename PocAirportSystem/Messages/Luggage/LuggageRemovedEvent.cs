namespace Messages.Luggage;

public class LuggageRemovedEvent
{
  public required string PassengerId { get; set; }
  public required List<string> LuggageId { get; set; }
}