using Domain;
using Domain.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class EventRegistrationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Registration> Registrations { get; set; } = default!;

    public EventRegistrationDbContext(DbContextOptions<EventRegistrationDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: DomainEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((DomainEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((DomainEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
}