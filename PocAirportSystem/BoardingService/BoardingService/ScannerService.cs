using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;

namespace BoardingService.BoardingService;

public class ScannerService : IScannerService
{
  private readonly IBoardingService _boardingService;
  private readonly IPassengerService _passengerService;
  private readonly IBus _bus;
  private readonly ILogger<ScannerService> _logger;

  public ScannerService(IBoardingService boardingService, ILogger<ScannerService> logger, IPassengerService passengerService, IBus bus)
  {
    _boardingService = boardingService;
    _logger = logger;
    _passengerService = passengerService;
    _bus = bus;
  }

  public async Task<bool> Scan(BoardingPassInput boardingPassInput)
  {
    ArgumentNullException.ThrowIfNull(_boardingService);
    var boarding = await _boardingService.GetBoardingByGateAndDateTimeWithPassengersAsync(boardingPassInput.GateNr, boardingPassInput.ScanTime);

    var passenger = boarding?.Passengers?
      .Where(passenger => passenger.PassengerId == boardingPassInput.PassengerId &&
                          passenger.CheckinNr == boardingPassInput.CheckinNr)
      .FirstOrDefault();

    if (passenger is null)
    {
      _logger.LogWarning("Boarding denied");
      await _bus.Publish(new PassengerDeniedEvent { PassengerId = boardingPassInput.PassengerId});
      return false;
    }

    await _passengerService.UpdatePassengerBoardingStatusAsync(passenger, hasBoarded: true);
    await _bus.Publish(new PassengerBoardedEvent { PassengerId = boardingPassInput.PassengerId});
    _logger.LogInformation("Boarding allowed");
    return true;
  }
}

public class PassengerDeniedEvent
{
  public required string PassengerId { get; set; }
}

public class PassengerBoardedEvent
{
  public required string PassengerId { get; set; }
}