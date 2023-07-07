using Microsoft.AspNetCore.Identity;

namespace Walmart.IDP.Entities;
public sealed class UserEntity : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<UserClaimEntity> Claims { get; set; } = new List<UserClaimEntity>();
}
