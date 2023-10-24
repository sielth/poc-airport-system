namespace BoardingService.Infrastructure.LuggageControl.Consumers
{
    public class Completed
    {
        public required string PassengerId { get; set; }
        public required int GateNumber {get; set;}
        public required string FlightNumber { get; set; }
        public required string CheckedInNumber { get; set; }
        public required int BaggageId { get; set; }
        
        public required double PlaneWeightRemaining { get; set; }



    }
}
