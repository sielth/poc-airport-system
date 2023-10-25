﻿using BoardingService.Infrastructure.Checkin;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Mapster;
using MassTransit;

namespace BoardingService.Infrastructure.Gate.Consumers;

public class GateAssignedConsumer : IConsumer<GateAssignedEvent>
{
  private readonly ICheckinCaller _checkinCaller;
  private readonly IBoardingService _boardingService;
  private readonly IBus _bus;

  public GateAssignedConsumer(ICheckinCaller checkinCaller, IBoardingService boardingService, IBus bus)
  {
    _checkinCaller = checkinCaller;
    _boardingService = boardingService;
    _bus = bus;
  }

  public async Task Consume(ConsumeContext<GateAssignedEvent> context)
  {
    var boarding = context.Message.Adapt<Boarding>();
    await _boardingService.AddBoardingAsync(boarding);

    await _bus.CreateDelayedMessageScheduler().SchedulePublish(context.Message.From.AddMinutes(-5),new CheckGateCommand
    {
      FlightNr = context.Message.FlightNr,
      GateNr = context.Message.GateNr,
      From = context.Message.From,
      To = context.Message.To
    });
  }
}