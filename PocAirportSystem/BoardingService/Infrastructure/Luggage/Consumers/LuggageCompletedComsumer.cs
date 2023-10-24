using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.Infrastructure.Luggage.Consumers
{
    public class LuggageCompletedComsumer : IConsumer<Completed>
    {
        private readonly IPassengerService _passengerservice;
        public LuggageCompletedComsumer(IPassengerService passengerService)
        {
           _passengerservice = passengerService;
        }
      //  public Task Consume(ConsumeContext<Completed> context)
      //  {   
            //var passenger = _passengerservice.GetPassengerByPassengerID
         //   {
           //     LuggageId = context.Message.BaggageId,
           //     Passengerid = context.Message.PassengerId,
            //    CheckinNr = context.Message.CheckedInNumber,
           //     Status = true
        //    };
      //  }
    }
}
