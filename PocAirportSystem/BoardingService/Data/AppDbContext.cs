using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace BoardingService.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) { }

  public DbSet<Boarding> Boardings { get; set; }
  public DbSet<Passenger> Passengers { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Boarding>()
      .HasKey(boarding => boarding.FlightNr);
    
    modelBuilder.Entity<Boarding>()
      .HasMany(boarding => boarding.Passengers)
      .WithOne(passenger =>  passenger.Boarding)
      .HasForeignKey(passenger => passenger.FlightNr)
      .IsRequired();
    
    modelBuilder.Entity<Passenger>()
      .HasAlternateKey(passenger => new { passenger.PassengerId, passenger.CheckinNr });

    modelBuilder.Entity<Passenger>()
      .HasOne(passenger => passenger.Boarding)
      .WithMany(boarding => boarding.Passengers)
      .HasForeignKey(passenger => passenger.FlightNr)
      .IsRequired();
  }
}