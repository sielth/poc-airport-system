using BoardingService.Models.BoardingAggregate;
using MassTransit;
using Messages;
using Messages.Boarding;
using Messages.Gate;

namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class CheckGateConsumer : IConsumer<CheckGateCommand>
{
  private readonly IHostEnvironment _environment;
  private readonly IBoardingService _boardingService;
  private readonly IBus _bus;
  private readonly ILogger<CheckGateConsumer> _logger;

  public CheckGateConsumer(IBoardingService boardingService, IBus bus, ILogger<CheckGateConsumer> logger, IHostEnvironment environment)
  {
    _boardingService = boardingService;
    _bus = bus;
    _logger = logger;
    _environment = environment;
  }

  public async Task Consume(ConsumeContext<CheckGateCommand> context)
  {
    var boarding = await _boardingService.GetBoardingByFlightNrAsync(context.Message.FlightNr);

    if (boarding is null)
    {
      _logger.LogWarning("Boarding not found");
    } 
    else if (boarding.GateNr == context.Message.GateNr && boarding.From == context.Message.From)
    {
      var boardingStart = context.Message.From;
      var lastCall = context.Message.To.AddMinutes(-10);
      var boardingEnd = context.Message.To;

      if (_environment.IsDevelopment())
      {
        boardingStart = DateTime.Now.AddSeconds(5);
        lastCall = DateTime.Now.AddSeconds(15);
        boardingEnd = DateTime.Now.AddSeconds(30);
      }
      
      await context.SchedulePublish(boardingStart, new UpdateBoardingStatusEvent
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        GateStatus = GateStatus.Boarding
      });
      // schedule last call
      await context.SchedulePublish(lastCall, new LastCallCommand
      {
        FlightNr = context.Message.FlightNr,
        GateNr = context.Message.GateNr
      });
      // schedule gate close
      await _bus.CreateDelayedMessageScheduler().SchedulePublish(boardingEnd, new UpdateBoardingStatusEvent
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        GateStatus = GateStatus.Closed
      });
    } 
    else if (boarding.GateNr != context.Message.GateNr || boarding.From != context.Message.From)
    {
      await context.SchedulePublish(context.Message.From, new CheckGateCommand
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        From = context.Message.From,
        To = context.Message.To
      });
    }
  }
}