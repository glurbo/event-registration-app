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
}