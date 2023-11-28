using BoardingService.Infrastructure.Checkin;
using BoardingService.Infrastructure.Gate.Consumers.CheckGate;
using BoardingService.Models.BoardingAggregate;
using Mapster;
using MassTransit;
using Messages.Gate;

namespace BoardingService.Infrastructure.Gate.Consumers.GateAssigned;

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>
{
  private readonly IHostEnvironment _environment;
  private readonly IBoardingService _boardingService;
  private readonly IBus _bus;

  public GateAssignedConsumer(IBoardingService boardingService, IBus bus, IHostEnvironment environment)
  {
    _boardingService = boardingService;
    _bus = bus;
    _environment = environment;
  }

  public async Task Consume(ConsumeContext<GateAssignedEvent> context)
  {
    var boardingStart = context.Message.GateEndTime.AddMinutes(-60);
    var boarding = new Boarding
    {
      FlightNr = context.Message.FlightNr,
      GateNr = context.Message.GateNr,
      From = boardingStart,
      To = context.Message.GateEndTime
    };
    
    await _boardingService.AddBoardingAsync(boarding);

    var scheduledTime = boardingStart.AddMinutes(-5);
    if (_environment.IsDevelopment()) scheduledTime = DateTime.Now.AddSeconds(5);
    
    await _bus.CreateDelayedMessageScheduler().SchedulePublish(scheduledTime, new CheckGateCommand
    {
      FlightNr = context.Message.FlightNr,
      GateNr = context.Message.GateNr,
      From = boardingStart,
      To = context.Message.GateEndTime
    });
  }
}