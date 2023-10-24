namespace BoardingService.BoardingService;

public interface IScannerService
{
  public Task<bool> Scan(BoardingPassInput boardingPassInput);
}