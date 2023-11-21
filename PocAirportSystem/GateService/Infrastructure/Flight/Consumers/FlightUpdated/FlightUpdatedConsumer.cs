using GateService.Models.DelaysAggregate;
using GateService.Models.GateAggregate;
using MassTransit;
using Messages.Flight;

namespace GateService.Infrastructure.Flight.Consumers.FlightUpdated;

public class FlightUpdatedConsumer : IConsumer<FlightUpdatedEvent>
{
  private readonly IGateService _gateService;
  private readonly IDelayService _delayService;

  public FlightUpdatedConsumer(IGateService gateService, IDelayService delayService)
  {
    _gateService = gateService;
    _delayService = delayService;
  }

  public async Task Consume(ConsumeContext<FlightUpdatedEvent> context)
  {
    var gate = await _gateService.GetGateByFlightNrAsync(context.Message.FlightId.ToString());

    if (gate is not null)
    {
      await _gateService.FreeGateAsync(gate.GateNr!.Value);
    }

    var thresholdMinutes = 90;
    if (context.Message.DepartureDate.AddMinutes(-thresholdMinutes) < DateTime.Now)
    {
      thresholdMinutes = (context.Message.DepartureDate - DateTime.Now).Minutes;
    }
    
    await _delayService.AddDelayAsync(new Delay
    {
      FlightNr = context.Message.FlightId.ToString(),
      NewFrom = context.Message.DepartureDate.AddMinutes(-thresholdMinutes),
      NewTo = context.Message.DepartureDate
    });
  }
}

// if flight is delayed (listen to routing key Journey.Updated.Boarding)
// if gate has been assigned to flight
// make gate avaliable again, schedule publish one and a half hour before
// if gate has not been assigned yet, save/update flight in db