namespace Walmart.Identity.Pages;
public static class IdentityConfiguration
{
    public static IEnumerable<IdentityResource> IdentityResources
        => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes
        => new List<ApiScope>()
        {
            new ApiScope(name: "walmart", displayName: "WalmartApiProducts"),
            new ApiScope(name: "read", displayName: "Read your data."),
            new ApiScope(name: "write", displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data.")
        };

    public static IEnumerable<Client> Clients
        => new List<Client>()
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "walmart",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:44339/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:44339/signout-callback-oidc" },
                AllowedScopes = new List<string>()
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "walmart"
                }
            }
        };

}
