using System.Reflection;
using Ardalis.SharedKernel;
using BoardingService.Models.BoardingAggregate;
using BoardingService.Models.PassengerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BoardingService.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;
  public DbSet<Boarding> Boardings => Set<Boarding>();
  public DbSet<Passenger> Passengers => Set<Passenger>();
  public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher)
  {
    _dispatcher = dispatcher;
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher is null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
      .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
      .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }


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
      .HasAlternateKey(passenger => new { passenger.PassengerId, passenger.CheckinNr });

    modelBuilder.Entity<Passenger>()
      .HasOne(passenger => passenger.Boarding)
      .WithMany(boarding => boarding.Passengers)
      .HasForeignKey(passenger => passenger.FlightNr)
      .IsRequired();
  }
}