using System.Reflection;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace GateService.Infrastructure.Data;

// TODO: Fill up
public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}