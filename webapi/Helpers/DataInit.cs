using DAL;
using Domain;
using Microsoft.AspNetCore.Identity;

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
        new Event { Id = Guid.NewGuid(), Name = "Fun event", MaxParticipants = 30, Date = new DateTime(2023, 12, 12) },
        new Event { Id = Guid.NewGuid(), Name = "Fun event 2", MaxParticipants = 10, Date = new DateTime(2023, 9, 5) },
        new Event { Id = Guid.NewGuid(), Name = "Fun event 3", MaxParticipants = 50, Date = new DateTime(2023, 10, 10) }
    };
}