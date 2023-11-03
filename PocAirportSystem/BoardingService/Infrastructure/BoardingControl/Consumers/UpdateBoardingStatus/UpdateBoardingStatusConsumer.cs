using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using MassTransit;
using Messages;
using Messages.Boarding;
using Messages.Luggage;

namespace BoardingService.Infrastructure.BoardingControl.Consumers.UpdateBoardingStatus;

public class UpdateBoardingStatusConsumer : IConsumer<UpdateBoardingStatusEvent>
{
  private readonly IBoardingService _boardingService;
  private readonly ILuggageService _luggageService;

  public UpdateBoardingStatusConsumer(IBoardingService boardingService, ILuggageService luggageService)
  {
    _boardingService = boardingService;
    _luggageService = luggageService;
  }

  public async Task Consume(ConsumeContext<UpdateBoardingStatusEvent> context)
  {
    var boarding = await _boardingService.GetBoardingByFlightNrAsync
      (context.Message.FlightNr);

    switch (context.Message.GateStatus)
    {
      case GateStatus.Boarding:
        return;
      case GateStatus.Closed:
      {
        var unboardedPassengers = boarding!.Passengers!.Where(passenger => passenger.Status = false)
          .ToList();
        if (unboardedPassengers.Any() is false) return;

        var lugaggesToUnboard = unboardedPassengers
          .Where(passenger => passenger.Luggages?.Any() is true)
          .SelectMany(passenger => passenger.Luggages!).ToList();

        foreach (var luggage in lugaggesToUnboard)
        {
          luggage.Status = false;
          await _luggageService.UpdateLuggageAsync(luggage);
          await context.Publish(new LuggageRemovedEvent
          {
            PassengerId = luggage.PassengerId,
            LuggageId = luggage.LuggageId
          });
        }

        break;
      }
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}