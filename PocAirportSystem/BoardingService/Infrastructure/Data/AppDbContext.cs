using System.Reflection;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.LuggageAggregate;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;

namespace BoardingService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Boarding> Boardings => Set<Boarding>();
    public DbSet<Luggage> Luggages => Set<Luggage>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Luggage>()
          .HasKey(luggage => luggage.LuggageId);
        
        modelBuilder.Entity<Luggage>()
            .HasOne(luggage => luggage.Passenger)
            .WithMany(passenger => passenger.Luggages)
            .HasForeignKey(luggage => new { luggage.PassengerId, luggage.CheckinNr });
        
        modelBuilder.Entity<Boarding>()
      .HasKey(boarding => boarding.FlightNr);

        modelBuilder.Entity<Boarding>()
          .HasMany(boarding => boarding.Passengers)
          .WithOne(passenger => passenger.Boarding)
          .HasForeignKey(passenger => passenger.FlightNr)
          .IsRequired();

        modelBuilder.Entity<Passenger>()
          .HasKey(passenger => new { passenger.PassengerId, passenger.CheckinNr });


        modelBuilder.Entity<Passenger>()
          .HasOne(passenger => passenger.Boarding)
          .WithMany(boarding => boarding.Passengers)
          .HasForeignKey(passenger => passenger.FlightNr)
          .IsRequired();
        modelBuilder.Entity<Passenger>()
            .HasMany(passenger => passenger.Luggages)
            .WithOne(Luggage => Luggage.Passenger);
    }
}