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
  private readonly IBoardingService _boardingService;
  private readonly IBus _bus;
  private readonly ILogger<ScannerService> _logger;
  private readonly IPassengerService _passengerService;
  private readonly IScannerService _scannerService;

  public ScannerServiceTest()
  {
    _boardingService = Substitute.For<IBoardingService>();
    _logger = Substitute.For<ILogger<ScannerService>>();
    _passengerService = Substitute.For<IPassengerService>();
    _bus = Substitute.For<IBus>();
    _scannerService = new ScannerService(_boardingService, _logger, _passengerService, _bus);
  }

  [Fact]
  public async Task ScannerService_Scan_UnitTest()
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
      Passengers = new List<Passenger>(),
      GateNr = new Random().Next(0,100),
      From = DateTime.UtcNow
    };
    boarding.Passengers.Add(passenger);
    
    var boardingPass = new BoardingPassInput
    {
      PassengerId = passenger.PassengerId,
      CheckinNr = passenger.CheckinNr,
      GateNr = (int)boarding.GateNr,
      ScanTime = (DateTime)boarding.From
    };
    
    _boardingService.GetBoardingByGateAndDateTimeWithPassengersAsync(boardingPass.GateNr, boardingPass.ScanTime)
      .Returns(boarding);
        
    // Act
    var scan = await _scannerService.Scan(boardingPass);

    // Assert
    Assert.True(scan);
  }
}