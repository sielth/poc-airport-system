using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoardingService.Infrastructure;
using BoardingService.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));

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
    factoryConfigurator.UseDelayedMessageScheduler();
    factoryConfigurator.ConfigureEndpoints(context);
  });
});

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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
  app.UseDefaultExceptionHandler(); // from FastEndpoints
  app.UseHsts();
}
app.UseFastEndpoints();
app.UseSwaggerGen();

app.UseAuthorization();
app.MapControllers();

// if (app.Environment.IsDevelopment())
// {
//   // Seed Database
//   using var scope = app.Services.CreateScope();
//   await SeedData.Initialize(scope.ServiceProvider);
// }

app.Run();