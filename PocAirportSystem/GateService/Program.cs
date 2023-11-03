var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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