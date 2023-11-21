using MassTransit;
using Messages.Flight;
using Messages.Gate;

namespace GateService.Infrastructure.Flight.Consumers.FlightCreated;

public class FlightCreatedConsumer : IConsumer<FlightCreatedEvent>
{
  private readonly IHostEnvironment _environment;

  public FlightCreatedConsumer(IHostEnvironment environment)
  {
    _environment = environment;
  }

  public async Task Consume(ConsumeContext<FlightCreatedEvent> context)
  {
    const int thresholdMinutes = 90;
    var scheduledTime = context.Message.DepartureDate.AddMinutes(-thresholdMinutes);
    
    if (_environment.IsDevelopment()) scheduledTime = DateTime.Now.AddSeconds(5);
    
    await context.SchedulePublish(scheduledTime,
      new AssignGateCommand
      {
        GateStartTime = context.Message.DepartureDate.AddMinutes(-thresholdMinutes),
        GateEndTime = context.Message.DepartureDate,
        FlightNr = context.Message.FlightId.ToString()
      });
  }
}