using FastEndpoints;
using MassTransit;

namespace BoardingService.BoardingService.Endpoints;

public class PassengerBoardedEventTestEndpoint : Endpoint<BoardRequest>
{
  public IBus? Bus { get; set; }

  public override void Configure()
  {
    Get("/api/test/passenger/{boarded}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(BoardRequest req, CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new PassengerBoardedEvent
    {
      CheckinNr = Guid.NewGuid().ToString(),
      PassengerId = Guid.NewGuid().ToString()
    }, ct);

    await SendOkAsync(ct);
  }
}

// public class Test : IConsumer<PassengerBoardedEvent>
// {
//   public Task Consume(ConsumeContext<PassengerBoardedEvent> context)
//   {
//     throw new NotImplementedException();
//   }
// }

public class BoardRequest
{
  public required int Boarded { get; set; }
}