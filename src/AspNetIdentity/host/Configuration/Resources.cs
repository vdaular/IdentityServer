/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServerHost.Configuration;

public class Resources
{
    // identity resources represent identity data about a user that can be requested via the scope parameter (OpenID Connect)
    public static readonly IEnumerable<IdentityResource> IdentityResources =
        new[]
        {
            // some standard scopes from the OIDC spec
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),

            // custom identity resource with some consolidated claims
            new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" })
        };

    // API scopes represent values that describe scope of access and can be requested by the scope parameter (OAuth)
    public static readonly IEnumerable<ApiScope> ApiScopes =
        new[]
        {
            // local API scope
            new ApiScope(LocalApi.ScopeName),

            // resource specific scopes
            new ApiScope("resource1.scope1"),
            new ApiScope("resource2.scope1"), 
            
            // a scope without resource association
            new ApiScope("scope3"),
            
            // a scope shared by multiple resources
            new ApiScope("shared.scope"),

            // a parameterized scope
            new ApiScope("transaction", "Transaction")
            {
                Description = "Some Transaction"
            }
        };

    // API resources are more formal representation of a resource with processing rules and their scopes (if any)
    public static readonly IEnumerable<ApiResource> ApiResources = 
        new[]
        {
            new ApiResource("resource1", "Resource 1")
            {
                ApiSecrets = { new Secret("secret".Sha256()) },

                Scopes = { "resource1.scope1", "shared.scope" }
            },
            
            // expanded version if more control is needed
            new ApiResource("resource2", "Resource 2")
            {
                ApiSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // additional claims to put into access token
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email
                },

                Scopes = { "resource2.scope1", "shared.scope" }
            }
        };
}
