namespace Walmart.Identity.DbContexts;
public sealed class WalmartIdentityDbContext : IdentityDbContext<
    UserEntity, RoleEntity, Guid, IdentityUserClaim<Guid>,
    UserRoleEntity, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public WalmartIdentityDbContext(DbContextOptions<WalmartIdentityDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>()
            .HasMany(u => u.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId);

        builder.Entity<RoleEntity>()
            .HasMany(u => u.UserRoles)
            .WithOne(r => r.Role)
            .HasForeignKey(ur => ur.RoleId);
    }

}
