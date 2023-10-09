using DAL;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace webapi.Helpers;

public static class DataInit
{
    public static void SeedIdentity(UserManager<AppUser> userManager)
    {
        var user = new AppUser()
        {
            Id = Guid.NewGuid(),
            UserName = "admin@app.com",
            Email = "admin@app.com",
            EmailConfirmed = true
        };
        var result = userManager.CreateAsync(user, "Foo.bar.1").Result;
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Cannot seed users, {result}");
        }
    }

    public static void SeedData(EventRegistrationDbContext context)
    {
        context.Events.AddRangeAsync(Events);
        context.SaveChangesAsync();
    }

    private static readonly Event[] Events =
    {
        new Event { Id = Guid.NewGuid(), Name = "Maie sünnipäevapidu", MaxParticipants = 50, Date = new DateTime(2023, 12, 12, 16, 0, 0) },
        new Event { Id = Guid.NewGuid(), Name = "Linuxi koolitus", MaxParticipants = 30, Date = new DateTime(2023, 10, 23, 12, 0, 0) },
        new Event { Id = Guid.NewGuid(), Name = "Viljandi jaanituli", MaxParticipants = 3000, Date = new DateTime(2023, 5, 23, 20, 0, 0) }
    };
}