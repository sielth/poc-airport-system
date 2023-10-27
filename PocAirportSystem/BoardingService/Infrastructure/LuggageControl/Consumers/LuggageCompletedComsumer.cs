using BoardingService.Models.LuggageAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Messages.Luggage;

namespace BoardingService.Infrastructure.LuggageControl.Consumers;

public class LuggageCompletedComsumer : IConsumer<LuggageCompleted>
{
  private readonly IPassengerService _passengerservice;
  private readonly ILuggageService _luggageService;
  private readonly ILogger<LuggageCompleted> _logger;

  public LuggageCompletedComsumer(IPassengerService passengerService, ILuggageService luggageService,
    ILogger<LuggageCompleted> logger)
  {
    _passengerservice = passengerService;
    _luggageService = luggageService;
    _logger = logger;
  }

  public async Task Consume(ConsumeContext<LuggageCompleted> context)
  {
    _logger.LogInformation(
      "Boarding luggage for passenger: {PassengerId}, {CheckedInNumber} at gate: {GateNr} for flight {FlightNr}",
      context.Message.PassengerId,
      context.Message.CheckedInNumber,
      context.Message.GateNumber,
      context.Message.FlightNumber);

    var passenger = await _passengerservice.GetPassengerByPassengerIdAsync
      (context.Message.PassengerId, context.Message.CheckedInNumber);

    // Check passenger's GateNr and FlightNr match with the luggage received
    // At this point passenger.Boarding is not null, since GateAssignedEvent has been sent,
    // thus a Boarding for this passenger has been created
    if (passenger.Boarding!.GateNr != context.Message.GateNumber &&
        passenger.Boarding!.FlightNr != context.Message.FlightNumber)
    {
      _logger.LogWarning(
        "Passenger's Boarding GateNr: {BoardingGateNr} or FlightNr: {BoardingFlightNr} does not match " +
        "with Luggage GateNr: {LuggageGateNr} or FlightNr: {LuggageFlightNr}",
        passenger.Boarding!.GateNr,
        passenger.Boarding!.FlightNr,
        context.Message.GateNumber,
        context.Message.FlightNumber);
      throw new ArgumentException(nameof(LuggageCompletedComsumer));
    }

    var luggage = new Luggage
    {
      CheckinNr = context.Message.CheckedInNumber,
      PassengerId = context.Message.PassengerId,
      LuggageId = context.Message.BaggageId.ToString(),
      Status = true,
      Passenger = passenger
    };

    await _luggageService.AddLuggageAsync(luggage);
    _logger.LogInformation("Luggage boarded for passenger: {PassengerId}, {CheckedInNumber} at gate: {GateNr} for flight {FlightNr}",
      context.Message.PassengerId,
      context.Message.CheckedInNumber,
      context.Message.GateNumber,
      context.Message.FlightNumber);
  }
}