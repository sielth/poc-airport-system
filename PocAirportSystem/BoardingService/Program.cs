using System.Reflection;
using BoardingService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));
builder.Services.AddMassTransit(configurator =>
{
  var assembly = typeof(Program).Assembly;
  
  configurator.AddConsumers(assembly);
  configurator.UsingRabbitMq((context, factoryConfigurator) =>
  {
    factoryConfigurator.Host("localhost", "/", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    factoryConfigurator.ConfigureEndpoints(context);
  });
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

app.Run();