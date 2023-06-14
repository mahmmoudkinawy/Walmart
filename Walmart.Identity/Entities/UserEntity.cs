namespace Walmart.Identity.Entities;
public sealed class UserEntity : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();
}
