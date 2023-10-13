using System.Reflection;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace BoardingService.Infrastructure.Data;

public class AppDbContext : DbContext
{
  public DbSet<Boarding> Boardings => Set<Boarding>();
  public DbSet<Passenger> Passengers => Set<Passenger>();
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
    modelBuilder.Entity<Boarding>()
      .HasKey(boarding => boarding.FlightNr);
    
    modelBuilder.Entity<Boarding>()
      .HasMany(boarding => boarding.Passengers)
      .WithOne(passenger =>  passenger.Boarding)
      .HasForeignKey(passenger => passenger.FlightNr)
      .IsRequired();
    
    modelBuilder.Entity<Passenger>()
      .HasKey(passenger => new { passenger.PassengerId, passenger.CheckinNr });

    modelBuilder.Entity<Passenger>()
      .HasOne(passenger => passenger.Boarding)
      .WithMany(boarding => boarding.Passengers)
      .HasForeignKey(passenger => passenger.FlightNr)
      .IsRequired();
  }
}