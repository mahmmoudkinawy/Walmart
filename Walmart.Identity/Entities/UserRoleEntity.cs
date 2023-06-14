namespace Walmart.Identity.Entities;
public sealed class UserRoleEntity : IdentityUserRole<Guid>
{
    public UserEntity User { get; set; }
    public RoleEntity Role { get; set; }
}