﻿namespace BoardingService.BoardingService;

public class BoardingPassInput
{
  public required string PassengerId { get; set; }
  public required string CheckinNr { get; set; }
  public required int GateNr { get; set; }
  public required DateTime ScanTime { get; set; }
}