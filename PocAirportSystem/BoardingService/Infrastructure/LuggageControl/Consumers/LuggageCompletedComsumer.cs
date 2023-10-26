using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.LuggageControl.Consumers
{
    public class LuggageCompletedComsumer : IConsumer<Completed>
    {
        private readonly IPassengerService _passengerservice;
        public LuggageCompletedComsumer(IPassengerService passengerService)
        {
           _passengerservice = passengerService;
        }
        public async Task Consume(ConsumeContext<Completed> context)
        {
            var passenger = await _passengerservice.GetPassengerByPassengerIdAsync(context.Message.PassengerId, context.Message.CheckedInNumber);
        }
    }
}
