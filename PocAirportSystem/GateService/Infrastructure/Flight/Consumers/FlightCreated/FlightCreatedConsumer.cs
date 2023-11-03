using GateService.Models.DelaysAggregate;
using GateService.Models.GateAggregate;
using MassTransit;
using Messages.Flight;
using Messages.Gate;

namespace GateService.Infrastructure.Flight.Consumers.FlightCreated;

public class FlightCreatedConsumer : IConsumer<FlightCreatedEvent>
{
  private readonly IDelayService _delayService;
  private readonly IGateService _gateService;

  public FlightCreatedConsumer(IDelayService delayService, IGateService gateService)
  {
    _delayService = delayService;
    _gateService = gateService;
  }

  public async Task Consume(ConsumeContext<FlightCreatedEvent> context)
  {
    const int thresholdMinutes = 90;
    var timeDifference = context.Message.DepartureDate - DateTime.Now;
    if (timeDifference.TotalMinutes > thresholdMinutes)
    {
      await context.SchedulePublish(context.Message.DepartureDate.AddMinutes(-thresholdMinutes), context.Message);
    }
    
    var delay = await _delayService.GetDelayByFlightNrAsync(context.Message.FlightId.ToString());
    if (delay is null) // Flight is still on time
    {
      var gate = await _gateService.GetAvailableGateAsync();
      ArgumentNullException.ThrowIfNull(gate); // TODO: No more gates available! Handle in a different way!

      await context.SchedulePublish(context.Message.DepartureDate.AddMinutes(-60), new GateAssignedEvent
      {
        From = context.Message.DepartureDate.AddMinutes(-60),
        To = context.Message.DepartureDate,
        FlightNr = context.Message.FlightId.ToString(),
        GateNr = gate.GateNr!.Value
      });

      await _delayService.DeleteDelayByFlightNrAsync(context.Message.FlightId.ToString());
    }
    else
    {
      
    }
  }
}