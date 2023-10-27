using BoardingService.Models.BoardingAggregate;
using MassTransit;
using Messages;
using Messages.Boarding;
using Messages.Gate;

namespace BoardingService.Infrastructure.Gate.Consumers.CheckGate;

public class CheckGateConsumer : IConsumer<CheckGateCommand>
{
  private readonly IBoardingService _boardingService;
  private readonly IBus _bus;
  private readonly ILogger<CheckGateConsumer> _logger;

  public CheckGateConsumer(IBoardingService boardingService, IBus bus, ILogger<CheckGateConsumer> logger)
  {
    _boardingService = boardingService;
    _bus = bus;
    _logger = logger;
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
      // schedule gate open
      await _bus.CreateDelayedMessageScheduler().SchedulePublish(context.Message.From, new UpdateBoardingStatusEvent
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        GateStatus = GateStatus.Boarding
      });
      // schedule last call
      await _bus.CreateDelayedMessageScheduler().SchedulePublish(context.Message.To.AddMinutes(-10), new LastCallCommand
      {
        FlightNr = context.Message.FlightNr,
        GateNr = context.Message.GateNr
      });
      // schedule gate close
      await _bus.CreateDelayedMessageScheduler().SchedulePublish(context.Message.To, new UpdateBoardingStatusEvent
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        GateStatus = GateStatus.Closed
      });
    } 
    else if (boarding.GateNr != context.Message.GateNr || boarding.From != context.Message.From)
    {
      await _bus.CreateDelayedMessageScheduler().SchedulePublish(context.Message.From, new CheckGateCommand
      {
        GateNr = context.Message.GateNr,
        FlightNr = context.Message.FlightNr,
        From = context.Message.From,
        To = context.Message.To
      });
    }
  }
}