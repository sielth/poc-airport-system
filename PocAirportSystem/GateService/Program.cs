using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoardingService.Infrastructure;
using BoardingService.Infrastructure.Data;
using GateService.Infrastructure.Flight.Consumers.FlightCreated;
using GateService.Infrastructure.Flight.Consumers.FlightUpdated;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
// builder.Services.AddDbContext<AppDbContext>(options =>
//   options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(configurator =>
{
  configurator.AddConsumers(assembly);
  configurator.UsingRabbitMq((context, factoryConfigurator) =>
  {
    factoryConfigurator.Host("localhost", "ucl", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    // Flight Created
    factoryConfigurator.ConfigureFlightCreated(context);

    // Flight Updated
    factoryConfigurator.ConfigureFlightUpdated(context);
    
    factoryConfigurator.UseDelayedMessageScheduler();
    factoryConfigurator.ConfigureEndpoints(context);
  });
});

// builder.Services.AddFastEndpoints();
// builder.Services.SwaggerDocument();

builder.Services.AddControllers();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  //Register all local types as interfaces
  containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

  //Register all local types as self
  containerBuilder.RegisterAssemblyTypes(assembly).AsSelf();

  // AutofacInfrastructureModule
  containerBuilder.RegisterModule(new AutofacInfrastructureModule(builder.Environment.IsDevelopment()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}
else
{
  // app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}
// app.UseFastEndpoints();
// app.UseSwaggerGen();

app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
  // Seed Database
  // using var scope = app.Services.CreateScope();
  // await SeedData.Initialize(scope.ServiceProvider);
}

app.Run();
// get travel (listen to FlightJourney routing key Journey.Created.Boarding)
// republish the message one hour and a half before departure
// check flight is still in time (call db)
// if yes
// assign gate one hour before departure => gate assigned event 
// delete evt. delays
// if not, republish
// max 20 gates

// if flight is delayed (listen to routing key Journey.Updated.Boarding)
// if gate has been assigned to flight
// make gate avaliable again, schedule publish one and a half hour before
// if gate has not been assigned yet, save/update flight in db