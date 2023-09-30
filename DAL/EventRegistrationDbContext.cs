using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class EventRegistrationDbContext : IdentityDbContext
{
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Registration> Registrations { get; set; } = default!;

    public EventRegistrationDbContext(DbContextOptions<EventRegistrationDbContext> options) : base(options)
    {
    }
}