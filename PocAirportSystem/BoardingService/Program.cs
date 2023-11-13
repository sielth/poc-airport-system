using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BoardingService.Infrastructure;
using BoardingService.Infrastructure.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .Enrich.WithProperty("Application", assembly.GetName().Name)
  .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
  .WriteTo.Console()
  .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"] 
                                                              ?? throw new InvalidOperationException()))
  {
    IndexFormat = $"{assembly.GetName().Name?.ToLower()}-logs-{builder.Environment.EnvironmentName.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
    AutoRegisterTemplate = true,
    NumberOfShards = 2,
    NumberOfReplicas = 1
  }));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(configurator =>
{
  configurator.AddConsumers(assembly);
  configurator.UsingRabbitMq((context, factoryConfigurator) =>
  {
    factoryConfigurator.Host("localhost", h =>
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