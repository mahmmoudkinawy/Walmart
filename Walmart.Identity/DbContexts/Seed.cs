namespace Walmart.Identity.DbContexts;
public static class Seed
{
    public static async Task SeedRoles(RoleManager<RoleEntity> roleManager)
    {
        ArgumentNullException.ThrowIfNull(nameof(roleManager));

        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        var roles = new List<RoleEntity>()
        {
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Constants.Roles.Admin
            },
            new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = Constants.Roles.Customer
            }
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }

    public static async Task SeedUsers(UserManager<UserEntity> userManager)
    {
        ArgumentNullException.ThrowIfNull(nameof(userManager));

        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var customers = new List<UserEntity>()
        {
            new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Customer",
                PhoneNumber = "01208534244",
                UserName = "bob@test.com",
                Email = "bob@test.com",
                PhoneNumberConfirmed = true
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Lisa",
                LastName = "Customer",
                PhoneNumber = "01208531212",
                UserName = "lisa@test.com",
                Email = "lisa@test.com",
                PhoneNumberConfirmed = true
            }
        };

        foreach (var customer in customers)
        {
            await userManager.CreateAsync(customer, "Pa$$w0rd");
            await userManager.AddToRoleAsync(customer, Constants.Roles.Customer);
        }

        var admin = new UserEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "Admin",
            PhoneNumber = "01208531214",
            UserName = "admin@test.com",
            Email = "admin@test.com",
            PhoneNumberConfirmed = true
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRoleAsync(admin, Constants.Roles.Admin);

    }

}
