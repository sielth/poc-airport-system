using FastEndpoints;
using Mapster;

namespace BoardingService.BoardingService.Endpoints;

public class BoardPassengerEndpoint : Endpoint<Request, Response>
{
   public IScannerService? ScannerService { get; set; }

   public override void Configure()
   {
     Post("/api/boarding");
     AllowAnonymous();
   }
   
   public override async Task HandleAsync(Request req, CancellationToken ct)
   {
     var boardingPassInput = req.Adapt<BoardingPassInput>();
     var hasBoarded = await ScannerService?.Scan(boardingPassInput)!;
     await SendAsync(new Response
     {
       PassengerId = boardingPassInput.PassengerId,
       HasBoarded = hasBoarded
     }, 200, ct);
   }
}

public class Response
{
  public required string PassengerId { get; set; }
  public required bool HasBoarded { get; set; }
}

public class Request
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
  public required int GateNr { get; set; }
  public required DateTime ScanTime { get; set; }
}