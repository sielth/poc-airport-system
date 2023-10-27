using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Messages;
using Messages.Boarding;
using Messages.Luggage;

namespace BoardingService.Infrastructure.BoardingControl.Consumers.UpdateBoardingStatus;

public class UpdateBoardingStatusConsumer : IConsumer<UpdateBoardingStatusEvent>
{
  private readonly IBoardingService _boardingService;
  private readonly IPassengerService _passengerService;
  private readonly IBus _bus;

  public UpdateBoardingStatusConsumer(IBoardingService boardingService, IBus bus, IPassengerService passengerService)
  {
    _boardingService = boardingService;
    _bus = bus;
    _passengerService = passengerService;
  }

  public async Task Consume(ConsumeContext<UpdateBoardingStatusEvent> context)
  {
    var boarding = await _boardingService.GetBoardingByFlightNrAsync
      (context.Message.FlightNr);

    if (context.Message.GateStatus is GateStatus.Closed)
    {
      var unboardedPassengers = boarding!.Passengers!.Where(passenger => passenger.Status = false)
        .ToList();
      if (unboardedPassengers.Any() is false) return;

      foreach (var unboardedPassenger in unboardedPassengers)
      {
        if (unboardedPassenger.Luggages is null || unboardedPassenger.Luggages.Any() is false) continue;
        foreach (var luggagesToUnboard in unboardedPassenger.Luggages)
        {
          luggagesToUnboard.Status = false;
        }
        
        // TODO: Fix and Uncomment
        // await _luggageService.UpdateLuggageAsync(luggage);
        // await _bus.Publish(new LuggageRemovedEvent { PassengerId = unboardedPassenger.PassengerId, LuggageId = luggagesToUnboard.LuggageId });
      }
    }
  }
}