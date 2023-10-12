using Ardalis.SharedKernel;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoardingService.Data;
using BoardingService.Gate;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));

// builder.Services.AddMassTransit(configurator =>
// {
//   var assembly = typeof(Program).Assembly;
//   
//   configurator.AddConsumers(assembly);
//   configurator.UsingRabbitMq((context, factoryConfigurator) =>
//   {
//     factoryConfigurator.Host("localhost", "ucl", h =>
//     {
//       h.Username("guest");
//       h.Password("guest");
//     });
//     factoryConfigurator.ConfigureEndpoints(context);
//   });
// });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterAssemblyTypes();
  
  // AutofacInfrastructureModule
  containerBuilder.RegisterGeneric(typeof(EfRepository<>))
    .As(typeof(IRepository<>))
    .As(typeof(IReadRepository<>))
    .InstancePerLifetimeScope();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var b = new Boarding()
{
  FlightNr = "lala",
  From = DateTime.Now,
  To = DateTime.Now,
  Gate = 1,
  Passengers = new List<Passenger>()
  {
    new Passenger()
    {
      FlightNr = "lala",
      CheckinNr = "dfkjs",
      PassengerId = "djskd",
      Status = true
    }
  }
};

var j = JsonConvert.SerializeObject(b);

using StreamReader reader = new("SeedBoarding.json");
var json = reader.ReadToEnd();
var boardings = JsonConvert.DeserializeObject<List<Boarding>>(json);
Console.ReadLine();
// Code for testing
// var bus = app.Services.GetRequiredService<IBus>();
// await bus.Publish(new GateAssignedEvent{
//   FlightNr = "TST1234",
//   GateNr = 1,
//   From = DateTime.Now.AddMinutes(-10),
//   To = DateTime.Now
// });


app.Run();