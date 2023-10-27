﻿using FastEndpoints;
using MassTransit;
using Messages.Gate;

namespace BoardingService.Infrastructure.Gate.Endpoints;

public class PublishGateAssignedEventTestEndpoint : EndpointWithoutRequest
{
  public IBus? Bus { get; set; }

  public override void Configure()
  {
    Get("/api/test/{flightNr}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    ArgumentNullException.ThrowIfNull(Bus);
    await Bus.Publish(new GateAssignedEvent
    {
      FlightNr = "0eb773dd-f2b0-4536-9f87-8a68598f9f17",
      GateNr = 86,
      From = DateTime.Now.AddMinutes(-10),
      To = DateTime.Now
    }, ct);

    await SendOkAsync(ct);
  }
}
