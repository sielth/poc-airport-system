using BoardingService.BoardingService;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Testing.UnitTest;

public class ScannerServiceTest
{
  private readonly IBoardingService _boardingServiceSub = Substitute.For<IBoardingService>();
  private readonly IBus _busSub = Substitute.For<IBus>();
  private readonly ILogger<ScannerService> _loggerSub = Substitute.For<ILogger<ScannerService>>();
  private readonly IPassengerService _passengerServiceSub = Substitute.For<IPassengerService>();
  private readonly IScannerService _scannerServiceSub;

  public ScannerServiceTest()
  {
    _scannerServiceSub = new ScannerService(_boardingServiceSub, _loggerSub, _passengerServiceSub, _busSub);
  }

  [Fact]
  public async Task TestScan()
  {
    // Arrange
    var passenger = new Passenger
    {
      PassengerId = Guid.NewGuid().ToString(),
      CheckinNr = Guid.NewGuid().ToString(),
      FlightNr = Guid.NewGuid().ToString()
    };
    var boarding = new Boarding
    {
      FlightNr = passenger.FlightNr,
      GateNr = 100,
      From = DateTime.UtcNow
    };
    boarding.Passengers?.Add(passenger);
    var boardingPass = new BoardingPassInput
    {
      CheckinNr = passenger.CheckinNr,
      GateNr = (int)boarding.GateNr,
      PassengerId = passenger.PassengerId,
      ScanTime = (DateTime)boarding.From
    };

    await _boardingServiceSub.AddBoardingAsync(boarding);

    // Act
    var scan = await _scannerServiceSub.Scan(boardingPass);

    // Assert
    Assert.True(scan);
  }
}