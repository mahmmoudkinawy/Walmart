namespace Walmart.Identity.Entities;
public sealed class RoleEntity : IdentityRole<Guid>
{
    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
}
