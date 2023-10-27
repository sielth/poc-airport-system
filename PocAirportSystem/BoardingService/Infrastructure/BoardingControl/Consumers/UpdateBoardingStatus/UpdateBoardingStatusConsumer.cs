using BoardingService.Infrastructure.Gate.Consumers.CheckGate;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Messages.Boarding;

namespace BoardingService.Infrastructure.BoardingControl.Consumers
{
  public class UpdateBoardingStatusConsumer : IConsumer<UpdateBoardingStatusEvent>
  {
    private readonly IBoardingService _boardingService;
    private readonly IBus _bus;
    private readonly IPassengerService _passengerService;

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

      var unboardedpassenger = boarding?.Passengers?.Where(passenger => passenger.Status = false).ToList();
      
      if (unboardedpassenger?.Any() is false) return;
      
      foreach (var passenger in unboardedpassenger)
      {
        var luggageIds = new List<string>();
        foreach (var luggage in passenger?.Luggages)
        {
          luggage.Status = false;
          luggageIds.Add(luggage.LuggageId);
        }

        await _passengerService.UpdatePassengerLuggageAsync(passenger);
        await _bus.Publish(new LuggageRemovedEvent { PassengerId = passenger.PassengerId, LuggageId = luggageIds });
      }
    }
  }
}