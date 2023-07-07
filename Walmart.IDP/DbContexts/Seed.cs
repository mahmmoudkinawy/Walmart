using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Walmart.IDP.Entities;
using Walmart.IDP.Helpers;

namespace Walmart.IDP.DbContexts;
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
                Id  = Guid.NewGuid(),
                Name = Constants.Roles.Admin
            },
            new RoleEntity
            {
                Id  = Guid.NewGuid(),
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
                LastName = "Bob",
                Email = "bob@test.com",
                UserName = "bob@test.com",
                PhoneNumber = "01208534244",
                Claims = new[]
                {
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.GivenName,
                        ClaimValue = "Bob"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.FamilyName,
                        ClaimValue = "Bob"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.PhoneNumber,
                        ClaimValue = "01208534244"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.Role,
                        ClaimValue = Constants.Roles.Customer
                    }
                }
            },
            new UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Lisa",
                LastName = "Ismael",
                Email = "lisa@test.com",
                UserName = "lisa@test.com",
                PhoneNumber = "012045958263",
                Claims = new[]
                {
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.GivenName,
                        ClaimValue = "Lisa"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.FamilyName,
                        ClaimValue = "Ismael"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.PhoneNumber,
                        ClaimValue = "012045958263"
                    },
                    new UserClaimEntity
                    {
                        ClaimType = JwtClaimTypes.Role,
                        ClaimValue = Constants.Roles.Customer
                    }
                }
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
            LastName = "Admona",
            Email = "admin@test.com",
            UserName = "admin@test.com",
            PhoneNumber = "01271128536",
            Claims = new[]
            {
                new UserClaimEntity
                {
                    ClaimType = JwtClaimTypes.GivenName,
                    ClaimValue = "Admin"
                },
                new UserClaimEntity
                {
                    ClaimType = JwtClaimTypes.FamilyName,
                    ClaimValue = "Admona"
                },
                new UserClaimEntity
                {
                    ClaimType = JwtClaimTypes.PhoneNumber,
                    ClaimValue = "01271128536"
                },
                new UserClaimEntity
                {
                    ClaimType = JwtClaimTypes.Role,
                    ClaimValue = Constants.Roles.Admin
                }
            }
        };

        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRoleAsync(admin, Constants.Roles.Admin);

    }

}
