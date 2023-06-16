using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Walmart.IDP;
public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles",
                "Your role(s)",
                new [] { JwtClaimTypes.Role })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client
                {
                    ClientId = "walmartwebapp",
                    ClientName = "Walmart Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris =
                    {
                        "https://localhost:7197/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7197/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    RequireConsent = true
                }
            };
}