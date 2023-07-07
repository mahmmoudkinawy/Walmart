using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Walmart.IDP.Entities;

namespace Walmart.IDP.DbContexts;
public sealed class WalmartIdentityDbContext : IdentityDbContext<
    UserEntity, RoleEntity, Guid, UserClaimEntity,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public WalmartIdentityDbContext(DbContextOptions<WalmartIdentityDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>()
            .HasMany(u => u.Claims)
            .WithOne(c => c.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
    }
}
