namespace BoardingService.BoardingService;

public interface IScannerService
{
  public bool Scan(BoardingPassInput boardingPassInput);
}