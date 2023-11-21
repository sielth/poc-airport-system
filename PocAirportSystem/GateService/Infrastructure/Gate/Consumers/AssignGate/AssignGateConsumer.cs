using GateService.Models.DelaysAggregate;
using GateService.Models.GateAggregate;
using MassTransit;
using Messages.Gate;

namespace GateService.Infrastructure.Gate.Consumers.AssignGate;

public class AssignGateConsumer : IConsumer<AssignGateCommand>
{
  private readonly IDelayService _delayService;
  private readonly IGateService _gateService;

  public AssignGateConsumer(IDelayService delayService, IGateService gateService)
  {
    _delayService = delayService;
    _gateService = gateService;
  }

  public async Task Consume(ConsumeContext<AssignGateCommand> context)
  {
    // If there is no delay on the flight
    var gate = await _gateService.GetAvailableGateAsync();
    ArgumentNullException.ThrowIfNull(gate); 
    
    await context.Publish(new GateAssignedEvent
    {
      GateStartTime = context.Message.GateStartTime, 
      GateEndTime = context.Message.GateEndTime, 
      FlightNr = context.Message.FlightNr,
      GateNr = gate.GateNr!.Value
    });

    // var delay = await _delayService.GetDelayByFlightNrAsync(context.Message.FlightNr);
    //
    // if (delay is null) // Flight is still on time
    // {
    //   var gate = await _gateService.GetAvailableGateAsync();
    //   ArgumentNullException.ThrowIfNull(gate); // TODO: No more gates available! Handle in a different way!
    //
    //   await context.SchedulePublish(context.Message.From, new GateAssignedEvent
    //   {
    //     From = context.Message.From,
    //     To = context.Message.To,
    //     FlightNr = context.Message.FlightNr,
    //     GateNr = gate.GateNr!.Value
    //   });
    //
    //   await _delayService.DeleteDelayByFlightNrAsync(context.Message.FlightNr);
    // }
    // else // If the flight is delayed
    // {
    //   await context.SchedulePublish(delay.NewFrom.AddMinutes(-thresholdMinutes), context.Message);
    //   await _delayService.DeleteDelayByFlightNrAsync(context.Message.FlightId.ToString());
    // }
  }
}