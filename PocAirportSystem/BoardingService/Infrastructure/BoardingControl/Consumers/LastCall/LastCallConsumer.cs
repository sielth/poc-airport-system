using MassTransit;
using Messages.Boarding;

namespace BoardingService.Infrastructure.BoardingControl.Consumers.LastCall;

public class LastCallConsumer : IConsumer<LastCallCommand>
{
  private readonly ILogger<LastCallConsumer> _logger;

  public LastCallConsumer(ILogger<LastCallConsumer> logger)
  {
    _logger = logger;
  }

  // TODO: Implement eventually
  public Task Consume(ConsumeContext<LastCallCommand> context)
  {
    _logger.LogInformation("Finding passengers that have not yet boarded on FlightNr {FlightNr} at Gate {GateNr}",
      context.Message.FlightNr, context.Message.GateNr);
    
    _logger.LogInformation("Notifying security with the names of the passengers that have not been boarded yet...");

    return Task.CompletedTask;
  }
}