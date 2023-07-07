using Microsoft.AspNetCore.Identity;

namespace Walmart.IDP.Entities;
public sealed class UserClaimEntity : IdentityUserClaim<Guid>
{
    public UserEntity User { get; set; }
}
