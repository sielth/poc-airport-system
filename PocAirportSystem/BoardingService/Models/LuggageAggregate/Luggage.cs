using Ardalis.SharedKernel;
using System.ComponentModel.DataAnnotations;
using Ardalis.SharedKernel;
using BoardingService.Models.PassengerAggregate;

namespace BoardingService.Models.LuggageAggregate
{
    public class Luggage : EntityBase, IAggregateRoot

    {
        [Key] public string? LuggageId { get; set; }
        public Passenger Passenger { get; set; }
        public string? PassengerId { get; set; }
        public string? CheckinNr { get; set; }
        public required bool Status { get; set; }
       



    }
}
