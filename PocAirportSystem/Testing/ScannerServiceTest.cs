using Ardalis.SharedKernel;
using BoardingService.BoardingService;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Testing;

public class ScannerServiceTest
{
    [Fact]
    public void TestScan()
    {
        // Arrange
        var boardingServiceSub = Substitute.For<IBoardingService>();
        var passengerServiceSub = Substitute.For<IPassengerService>();
        var loggerSub = Substitute.For<ILogger<ScannerService>>();
        var busSub = Substitute.For<IBus>();

        var passenger = new Passenger {
            PassengerId = "1bfa2920-a26b-4333-a0a8-173268534418",
            CheckinNr = "981906a7-0401-44b0-bd67-a3783431798e",
            FlightNr = "10a6a09d-1a4e-41f2-9421-68c80e4fea04"
        };
    }
}