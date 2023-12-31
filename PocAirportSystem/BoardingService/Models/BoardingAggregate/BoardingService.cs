﻿using Ardalis.SharedKernel;
using BoardingService.Models.BoardingAggregate.Specifications;

namespace BoardingService.Models.BoardingAggregate;

public class BoardingService : IBoardingService
{
  private readonly IRepository<Boarding> _repository;

  public BoardingService(IRepository<Boarding> repository)
  {
    _repository = repository;
  }

  public async Task<Boarding> AddBoardingAsync(Boarding boarding) =>
    await _repository.AddAsync(boarding);

  public async Task<IEnumerable<Boarding>> ListBoardingsAsync() => await _repository.ListAsync();
  public async Task<Boarding> GetBoardingByGateAndDateTimeWithPassengersAsync(int gate, DateTime scanTime)
  {
    var boarding = await _repository.FirstOrDefaultAsync(
      new BoardingByGateAndDateTimeWithPassengers(gate, scanTime));
    
    ArgumentNullException.ThrowIfNull(boarding);
    return boarding;
  }
  public async Task<Boarding?> GetBoardingByFlightNrAsync(string flightNr)
  {
    var boarding = await _repository.FirstOrDefaultAsync(
      new BoardingByFlightNr(flightNr));
  
    return boarding;
  }
  public async Task UpdateBoardingAsync(Boarding boarding) 
  {
    ArgumentNullException.ThrowIfNull(boarding.FlightNr);
    var boardingOld = await GetBoardingByFlightNrAsync(boarding.FlightNr);
    ArgumentNullException.ThrowIfNull(boardingOld);
    
    // update the old boarding info
    boardingOld.GateNr = boarding.GateNr;
    boardingOld.From = boarding.From;
    boardingOld.To = boarding.To;

    await _repository.UpdateAsync(boardingOld);
    await _repository.SaveChangesAsync();
  }
}