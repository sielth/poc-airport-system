using BoardingService.Models.BoardingAggregate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BoardingService.Infrastructure.Data;

public static class SeedData
{
  public static async Task Initialize(IServiceProvider serviceProvider)
  {
    await using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()); 
    
    await dbContext.Database.EnsureCreatedAsync();
    
    if (dbContext.Boardings.Any()) return; // DB has been seeded
    if (dbContext.Passengers.Any()) dbContext.Passengers.RemoveRange();
    
    await PopulateTestData(dbContext);
  }

  private static async Task PopulateTestData(DbContext dbContext)
  {
    using StreamReader reader = new("Infrastructure/Data/SeedBoarding.json");
    var json = await reader.ReadToEndAsync();
    var boardings = JsonConvert.DeserializeObject<List<Boarding>>(json);

    
    await dbContext.AddRangeAsync(boardings);
    await dbContext.SaveChangesAsync();
  }
}