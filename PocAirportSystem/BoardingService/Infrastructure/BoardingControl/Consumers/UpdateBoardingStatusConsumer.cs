using BoardingService.Infrastructure.Gate.Consumers.CheckGate;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.BoardingAggregate.Specifications;
using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.BoardingControl.Consumers
{
    public class UpdateBoardingStatusConsumer : IConsumer<UpdateBoardingStatusEvent>
    {
        private readonly IBoardingService _boardingService;
        private readonly IBus _bus; 
        private readonly IPassengerService _passengerService;
        public UpdateBoardingStatusConsumer(IBoardingService boardingService,IBus bus)
        {
            _boardingService = boardingService;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<UpdateBoardingStatusEvent> context
        {

            var boarding = await _boardingService.GetBoardingByFlightNrAsync
                (context.Message.FlightNr);

            var unboardedpassenger = boarding.Passengers.
                Where(passenger => passenger.Status = false).ToList();


            foreach (var passenger in unboardedpassenger)

            {   var luggageId = new List<string>();
                foreach (var luggage in passenger.Luggages)
                {
                    luggage.Status = false;
                    luggageId.Add(luggage.LuggageId);
                }
                await _passengerService.UpdatePassengerLuggageAsync(passenger);
                await _bus.Publish(new LuggageRemoved
                { passengerId = passenger.PassengerId, luggageId = luggageId });
                
                
            }

            


        }
    }
}
