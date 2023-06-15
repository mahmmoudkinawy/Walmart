// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace Walmart.IDP;

public class TestUsers
{
    public static List<TestUser> Users => new()
    {
        new TestUser
        {
            SubjectId = Guid.NewGuid().ToString(),
            Username = "bob",
            Password ="Pa$$w0rd",
            Claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.GivenName, "Bob"),
                new Claim(JwtClaimTypes.FamilyName, "Mahmoud")
            }
        },
        new TestUser
        {
            SubjectId = Guid.NewGuid().ToString(),
            Username = "lisa",
            Password ="Pa$$w0rd",
            Claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.GivenName, "lisa"),
                new Claim(JwtClaimTypes.FamilyName, "Mo")
            }
        }
    };
}